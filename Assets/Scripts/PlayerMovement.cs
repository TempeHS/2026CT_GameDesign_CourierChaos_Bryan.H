using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float csgoMaxSpeed = 14f;
    public float csgoGroundAccel = 120f;
    public float csgoAirAccel = 80f;
    public float csgoFriction = 8f;

    [Header("Jump")]
    public float jumpForce = 18f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;
    [Range(0f, 1f)] public float jumpCutMultiplier = 0.5f;

    [Header("Jump Limits")]
    public int maxAirJumps = 3;
    private int airJumpsUsed;

    [Header("Dash")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.3f;
    public int maxAirDashes = 1;

    [Header("Slide (Ground)")]
    public float slideSpeed = 16f;
    public float slideDuration = 0.4f;
    public float slideFriction = 0.25f;

    [Header("Wall Slide / Wall Jump")]
    public bool enableWallSlide = true;
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 18f;
    public float wallJumpHorizontalBoost = 10f;

    [Header("Checks")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform wallCheck;
    public float wallCheckDistance = 0.3f;
    public LayerMask wallLayer;

    [Header("State")]
    public bool isFrozen;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isSliding;
    private bool isDashing;
    private bool isWallSliding;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private float lastDashTime;
    private int airDashesUsed;
    private bool jumpPressed;
    private bool jumpHeld;
    private bool dashPressed;
    private bool slidePressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        CreateMissingChecks();
    }

    private void CreateMissingChecks()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        float bottomOffset = -0.5f;
        float sideOffset = 0.5f;

        if (playerCollider != null)
        {
            bottomOffset = playerCollider.offset.y - playerCollider.bounds.extents.y;
            sideOffset = playerCollider.offset.x + playerCollider.bounds.extents.x;
        }

        if (groundCheck == null)
        {
            Transform existingCheck = transform.Find("GroundCheck");
            groundCheck = existingCheck != null
                ? existingCheck
                : CreateCheck("GroundCheck", new Vector3(0f, bottomOffset, 0f));
        }

        if (wallCheck == null)
        {
            Transform existingCheck = transform.Find("WallCheck");
            wallCheck = existingCheck != null
                ? existingCheck
                : CreateCheck("WallCheck", new Vector3(sideOffset, 0f, 0f));
        }

        if (anim == null)
            Debug.LogWarning("PlayerMovement could not find an Animator. Movement will work without animations.", this);
    }

    private Transform CreateCheck(string checkName, Vector3 localPosition)
    {
        GameObject checkObject = new GameObject(checkName);
        checkObject.transform.SetParent(transform);
        checkObject.transform.localPosition = localPosition;
        return checkObject.transform;
    }

    private void Update()
    {
        if (isFrozen)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        dashPressed = Input.GetButtonDown("Fire3");
        slidePressed = Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.S);

        if (jumpPressed)
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter = Mathf.Max(jumpBufferCounter - Time.deltaTime, 0f);

        UpdateAnimationParameters();
    }

    private void FixedUpdate()
    {
        if (isFrozen || groundCheck == null || wallCheck == null)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        bool touchingWall = Physics2D.Raycast(
            wallCheck.position,
            facingRight ? Vector2.right : Vector2.left,
            wallCheckDistance,
            wallLayer);

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            airJumpsUsed = 0;
            airDashesUsed = 0;
        }
        else
        {
            coyoteCounter -= Time.fixedDeltaTime;
        }

        isWallSliding = enableWallSlide &&
                         !isGrounded &&
                         touchingWall &&
                         rb.linearVelocity.y < 0f &&
                         Mathf.Abs(moveInput) > 0.1f &&
                         !isDashing;

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }

        if (isWallSliding && jumpPressed)
        {
            float direction = facingRight ? -1f : 1f;
            rb.linearVelocity = new Vector2(direction * wallJumpHorizontalBoost, 0f);
            rb.AddForce(
                new Vector2(direction * wallJumpHorizontalBoost, wallJumpForce),
                ForceMode2D.Impulse);

            isWallSliding = false;
            return;
        }

        bool canGroundJump = coyoteCounter > 0f && !isWallSliding;
        bool canAirJump = !isGrounded && airJumpsUsed < maxAirJumps;

        if (jumpBufferCounter > 0f && (canGroundJump || canAirJump) && !isDashing)
        {
            Jump();

            if (!canGroundJump)
                airJumpsUsed++;

            jumpBufferCounter = 0f;
        }

        if (!jumpHeld && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * jumpCutMultiplier);
        }

        if (dashPressed && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            bool canDash = isGrounded || airDashesUsed < maxAirDashes;

            if (canDash)
            {
                if (!isGrounded)
                    airDashesUsed++;

                StartCoroutine(DashCoroutine());
            }
        }

        if (slidePressed && isGrounded && Mathf.Abs(moveInput) > 0.1f && !isSliding && !isDashing)
            StartCoroutine(SlideCoroutine());

        if (!isDashing && !isSliding && !isWallSliding)
        {
            if (isGrounded)
            {
                ApplyGroundFriction();
                CSGOAccelerate(moveInput, csgoMaxSpeed, csgoGroundAccel);
            }
            else
            {
                CSGOAirAccelerate(moveInput, csgoMaxSpeed, csgoAirAccel);
            }

            if (moveInput > 0f && !facingRight)
                Flip();
            else if (moveInput < 0f && facingRight)
                Flip();
        }
    }

    private void Jump()
    {
        coyoteCounter = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (anim != null)
            anim.SetTrigger("Jump");
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        lastDashTime = Time.time;

        if (anim != null)
            anim.SetTrigger("Dash");

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        if (dashDirection == Vector2.zero)
            dashDirection = facingRight ? Vector2.right : Vector2.left;

        dashDirection.Normalize();

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;

        if (anim != null)
            anim.SetBool("Sliding", true);

        float slideDirection = facingRight ? 1f : -1f;
        float elapsed = 0f;

        while (elapsed < slideDuration && isGrounded)
        {
            float speed = Mathf.Lerp(slideDirection * slideSpeed, 0f, slideFriction * elapsed);
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        if (anim != null)
            anim.SetBool("Sliding", false);

        isSliding = false;
    }

    private void CSGOAccelerate(float wishDirection, float wishSpeed, float acceleration)
    {
        float currentSpeed = rb.linearVelocity.x * wishDirection;
        float addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0f)
            return;

        float accelerationSpeed = acceleration * Time.fixedDeltaTime * wishSpeed;
        accelerationSpeed = Mathf.Min(accelerationSpeed, addSpeed);

        rb.linearVelocity += new Vector2(accelerationSpeed * wishDirection, 0f);
    }

    private void CSGOAirAccelerate(float wishDirection, float wishSpeed, float acceleration)
    {
        CSGOAccelerate(wishDirection, wishSpeed, acceleration);
    }

    private void ApplyGroundFriction()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);

        if (speed < 0.1f)
            return;

        float drop = speed * csgoFriction * Time.fixedDeltaTime;
        float newSpeed = Mathf.Max(speed - drop, 0f);

        rb.linearVelocity = new Vector2(
            newSpeed * Mathf.Sign(rb.linearVelocity.x),
            rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0f, facingRight ? 0f : 180f, 0f);
    }

    private void UpdateAnimationParameters()
    {
        if (anim == null)
            return;

        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("WallSlide", isWallSliding);
        anim.SetBool("Dashing", isDashing);
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 direction = facingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + direction * wallCheckDistance);
        }
    }
}
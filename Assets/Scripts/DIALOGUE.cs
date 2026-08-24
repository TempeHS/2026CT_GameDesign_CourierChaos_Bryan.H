using System.Collections;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private GameObject dialogueBox;

    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

    private int index;
    private bool isTyping;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();

        if (dialogueBox == null)
            dialogueBox = gameObject;
    }

    private void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("Dialogue needs a TextMeshProUGUI text component assigned.", this);
            enabled = false;
            return;
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialogue has no lines assigned.", this);
            FinishDialogue();
            return;
        }

        StartDialogue();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !enabled)
            return;

        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            textComponent.text = lines[index];
            isTyping = false;
            return;
        }

        NextLine();
    }

    private void StartDialogue()
    {
        index = 0;
        dialogueBox.SetActive(true);

        Debug.Log("Freezing player: dialogue started");

        if (playerMovement != null)
            playerMovement.isFrozen = true;
        else
            Debug.LogWarning("Dialogue could not find PlayerMovement; player was not frozen.", this);

        StartTypingCurrentLine();
    }

    private void StartTypingCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char character in lines[index])
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartTypingCurrentLine();
            return;
        }

        FinishDialogue();
    }

    private void FinishDialogue()
    {
        Debug.Log("Unfreezing player: dialogue finished");

        if (playerMovement != null)
            playerMovement.isFrozen = false;

        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    public void FreezeForSeconds(float seconds)
    {
        StartCoroutine(FreezeTimer(seconds));
    }

    private IEnumerator FreezeTimer(float seconds)
    {
        if (playerMovement == null)
        {
            Debug.LogWarning("Timed freeze failed: PlayerMovement was not found.", this);
            yield break;
        }

        Debug.Log("Timed freeze started");
        playerMovement.isFrozen = true;

        yield return new WaitForSeconds(seconds);

        playerMovement.isFrozen = false;
        Debug.Log("Timed freeze ended");
    }
}
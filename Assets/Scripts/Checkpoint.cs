using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector2 savedPosition = Vector2.zero;

    public AudioSource checkpointSound;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            savedPosition = collision.transform.position;

            if (!activated)
            {
                activated = true;

                if (checkpointSound != null)
                {
                    checkpointSound.Play();
                }
            }

            Debug.Log("Checkpoint saved: " + savedPosition);
        }
    }
}
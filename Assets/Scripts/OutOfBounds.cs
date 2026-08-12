using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour
{
    public Transform teleportPoint;
    public ScreenFader fader;
    public GameObject messageUI;

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportSequence(collision.transform));
        }
    }

    private IEnumerator TeleportSequence(Transform player)
    {
        isTeleporting = true;

        
        messageUI.SetActive(true);

        
        yield return StartCoroutine(fader.FadeToBlack());

        
        player.position = teleportPoint.position;

        
        messageUI.SetActive(false);

        
        yield return StartCoroutine(fader.FadeFromBlack());

        isTeleporting = false;
    }
}

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnPlayer(collision.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        if (Checkpoint.savedPosition != Vector2.zero)
        {
            player.transform.position = Checkpoint.savedPosition;
        }
        else
        {
            Debug.Log("No checkpoint saved. Player cannot respawn.");
        }
    }
}
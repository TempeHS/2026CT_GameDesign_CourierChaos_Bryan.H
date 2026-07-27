using UnityEngine;
using TMPro;

using System.Collections;

public class ParcelController : MonoBehaviour
{
    public TextMeshProUGUI pickupText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickupText.text = "You picked up the parcel!";
            pickupText.gameObject.SetActive(true);

            Destroy(gameObject);
            StartCoroutine(HideText());
        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(2f);
        pickupText.gameObject.SetActive(false);
    }
}

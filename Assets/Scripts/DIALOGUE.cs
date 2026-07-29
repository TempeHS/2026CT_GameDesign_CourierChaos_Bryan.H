using UnityEngine;
using TMPro;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public GameObject dialogueBox;
    public PlayerMovement playerMovement;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Skip typing animation
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
                if (index == 3)
                {
                    playerMovement.isFrozen = false;
                }

            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
        playerMovement.isFrozen = true;
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
        }

    }
    public void FreezeForSeconds(float seconds)
{
    StartCoroutine(FreezeTimer(seconds));
}

    private IEnumerator FreezeTimer(float seconds)
    {
    isFrozen = true;
    yield return new WaitForSeconds(seconds);
    isFrozen = false;
    }
}

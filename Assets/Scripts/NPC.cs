using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData;
    public GameObject dialougePannel;
    public TMP_Text dialougeText, nameText;
    public Image portraitImage;

    private int dialougeIndex;
    private bool isTyping;
    private bool isDialougeActive;

    public bool CanInteract()
    {
        return !isDialougeActive;
    }

    public void Interact()
    {
        if (dialougeData == null)
            return;

        if (PauseController.IsGamePaused && !isDialougeActive)
            return;

        if (isDialougeActive)
        {
            if (isTyping)
            {
                StopAllCoroutines();

                dialougeText.SetText(
                    dialougeData.dialougeLines[dialougeIndex]
                );

                isTyping = false;
            }
            else
            {
                DisplayNextLine();
            }
        }
        else
        {
            StartDialouge();
        }
    }

    private void StartDialouge()
    {
        if (
            dialougeData.dialougeLines == null ||
            dialougeData.dialougeLines.Length == 0
        )
        {
            return;
        }

        isDialougeActive = true;
        dialougeIndex = 0;

        if (nameText != null)
            nameText.SetText(dialougeData.npcName);

        if (portraitImage != null)
            portraitImage.sprite = dialougeData.npcPortrait;

        if (dialougePannel != null)
            dialougePannel.SetActive(true);

        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        if (
            dialougeData.dialougeLines == null ||
            dialougeIndex >= dialougeData.dialougeLines.Length
        )
        {
            EndDialouge();
            yield break;
        }

        isTyping = true;
        dialougeText.SetText("");

        string currentLine =
            dialougeData.dialougeLines[dialougeIndex];

        foreach (char letter in currentLine)
        {
            dialougeText.text += letter;

            yield return new WaitForSecondsRealtime(
                dialougeData.typingSpeed
            );
        }

        isTyping = false;

        if (
            dialougeData.autoProgressLines != null &&
            dialougeIndex < dialougeData.autoProgressLines.Length &&
            dialougeData.autoProgressLines[dialougeIndex]
        )
        {
            yield return new WaitForSecondsRealtime(
                dialougeData.autoProgressDelay
            );

            DisplayNextLine();
        }
    }

    private void DisplayNextLine()
    {
        dialougeIndex++;

        if (
            dialougeData.dialougeLines != null &&
            dialougeIndex < dialougeData.dialougeLines.Length
        )
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialouge();
        }
    }

    public void EndDialouge()
    {
        StopAllCoroutines();

        isTyping = false;
        isDialougeActive = false;

        if (dialougePannel != null)
            dialougePannel.SetActive(false);

        PauseController.SetPause(false);
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData; 
    public GameObject dialougePannel;
    public TMP_Text dialougeText, nameText; 
    public Image portraitImage; 

    private int dialougeIndex; 
    private bool isTyping, isDialougeActive; 

    public bool CanInteract()
    {
        return !isDialougeActive; 
    }
    public void Interact()
    {
        if(dialougeData == null || (PauseController.IsGamePaused && !isDialougeActive))
            return; 

        if(isDialougeActive) 
        {
            //Next Line
        }
        else
        {
            //StartDialouge
        }
    }
    void StartDialouge()
    {
        isDialougeActive = true; 
        dialougeIndex = 0; 

        nameText.SetText(dialougeData.npcName);
        portraitImage.sprite = dialougeData.npcPortrait; 

        dialougePannel.SetActive(true);
        PauseController.SetPause(true); 

        //TypeLine 
    }

    IEnumorator TypeLine()
    {
        isTyping = true; 
        dialougeText.SetText(""); 

        foreach(char letter in dialougeData.dialougeLines[dialougeIndex])
        {
            dialougeText.text += letter; 
            yeild return new WaitForSeconds(dialougeData.typingSpeed); 
        }

        isTyping = false; 

        if(dialougeData.autoProgressLines.Length > dialougeIndex && dialougeData.autoProgressLines[dialougeIndex])
        {
            yield return new WaitForSeconds(dialougeData.autoProgressDelay); 
            //DisplayNextLine
        }
    }
}

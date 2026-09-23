using UnityEngine;

public class TipsMenuController : MonoBehaviour
{
    public GameObject tipsCanvas;

    void Start()
    {
        tipsCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!tipsCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }

            tipsCanvas.SetActive(!tipsCanvas.activeSelf);
        }
    }
}


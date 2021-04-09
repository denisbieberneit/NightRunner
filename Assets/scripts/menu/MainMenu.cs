using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject levelSelectionCanvas;

    public void OnButtonMenu()
    {
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        levelSelectionCanvas.SetActive(false);
    }

    public void OnButtonStart()
    {
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        levelSelectionCanvas.SetActive(true);
    }

    public void OnMuteAudioButton()
    {
        if (AudioManager.instance.muted)
        {
            AudioManager.instance.ActivateAll();
        }
        else
        {
            AudioManager.instance.MuteAll();
        }
    }

    public void OnMMogaButton()
    {
        Application.OpenURL("https://www.mmoga.com/");
    }
}

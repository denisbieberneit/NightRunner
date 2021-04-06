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
    
    public void OnButtonMenu()
    {
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}

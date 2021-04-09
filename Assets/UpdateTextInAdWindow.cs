using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateTextInAdWindow : MonoBehaviour
{
    public Text coins;
    public Text afterAdCoins;
    public GameObject deathScreen;
    public GameObject adsScreen;

    public void OnCloseButton()
    {
        deathScreen.SetActive(true);
        adsScreen.SetActive(false);
    }
    private void Start()
    {
        coins.text = coins.text + PlayerPrefs.GetInt("PlayerCoinsRun");
        afterAdCoins.text = afterAdCoins.text + (PlayerPrefs.GetInt("PlayerCoinsRun") * 2);
    }
}

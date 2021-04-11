using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelItem : MonoBehaviour
{
    [SerializeField] public string levelName;
    [SerializeField] public int levelCost;
    [SerializeField] public bool owned;

    private Button button;
    public  Button unlockButton;

    private void Start()
    {
        button = GetComponent<Button>();
        unlockButton.GetComponentInChildren<Text>().text = unlockButton.GetComponentInChildren<Text>().text + " " + levelCost + " COINS";
        button.interactable = true;
        unlockButton.gameObject.SetActive(false);
        if (!owned)
        {
            button.interactable = false;
            unlockButton.gameObject.SetActive(true);
        }
    }

    public void OnBuyLevelButton()
    {
        int coins = PlayerPrefs.GetInt("PlayerCoins");

        if (coins >= levelCost)
        {
            owned = true;
            PlayerPrefs.SetInt("PlayerCoins", coins - levelCost);
            unlockButton.gameObject.SetActive(false);
            button.interactable = true;
        }
        else
        {
            // could not buy level
        }
    }
}

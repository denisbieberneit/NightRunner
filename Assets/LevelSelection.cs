using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Text coins;

    private void Update()
    {
        coins.text = "Coins: " + PlayerPrefs.GetInt("PlayerCoins");
    }

    private void Awake()
    {
        LevelData data = SaveSystem.LoadLevels();
        if (data != null)
        {
            LevelItem[] items = GetComponentsInChildren<LevelItem>();
            foreach (string levelOwned in data.ownedLevelNames)
            {
                foreach (LevelItem item in items)
                {
                    if (item.name.Equals(levelOwned))
                    {
                        item.owned = true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No Leveldata");
        }
    }
}

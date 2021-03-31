using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string[] ownedSkinNames;
    
    public InventoryData(Inventory inventory)
    {
        int i = 0;
        foreach (InventoryItem item in inventory.GetSkins())
        {
            if (item.owned)
            {
                i++;
            }
        }
        ownedSkinNames = new string[i];
        i = 0;
        foreach (InventoryItem item in inventory.GetSkins())
        {
            if (item.owned)
            {
                ownedSkinNames[i] = item.skin.GetComponentInChildren<Player>().playerName;
                i++;
            }
        }
    }
}

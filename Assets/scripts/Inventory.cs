using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryItem[] skins;
    public Transform spawn;
    public int skinInPreview;

    public void Awake()
    {
        skins = FindObjectsOfType<InventoryItem>();
        InventoryData data = SaveSystem.LoadInventory();
        if (data != null)
        {
            foreach (string skinOwned in data.ownedSkinNames)
            {
                foreach (InventoryItem item in skins)
                {
                    Debug.Log("Checking " + skinOwned);
                    if (item.skin.GetComponentInChildren<Player>().playerName.Equals(skinOwned))
                    {
                        item.owned = true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No inventorydata");
        }


        spawn = FindObjectOfType<Spawn>().getTransform();
        int i = 0;


        //TESTING SKINS
        //PlayerPrefs.SetString("PlayerSkin","Wizard");
        
        
        
        
        string playerSkin = PlayerPrefs.GetString("PlayerSkin");
        if (playerSkin == "" || playerSkin == null)
        {
            playerSkin = "Whity";
        }
        if (playerSkin != null)
        {
            UseSkinFromCollectionByName(playerSkin);
        }
        foreach (InventoryItem item in skins)
        {
            if (item.inUse)
            {
                UseSkinFromCollection(item.skin.GetComponent<Player>().playerName);
                skinInPreview = i;
            }
            i = i+1;
        }
    }

    public void UseSkinFromCollection(string name)
    {
        Destroy(GameObject.Find("Player"));
        foreach (InventoryItem item in skins)
        {
            Player player = item.skin.GetComponent<Player>();
            if (player.playerName.ToLower().Equals(name.ToLower()))
            {
                Instantiate(item.skin,spawn);
                break;
            }
        }
        
    }

    public InventoryItem GetNextSkin()
    {
        skinInPreview = skinInPreview + 1;
        int skinCount = skins.Length - 1;
        if (skinInPreview > skinCount)
        {
            skinInPreview = 0;
            return skins[0];
        }else if (skinInPreview < 0)
        {
            skinInPreview = skinCount;
            return skins[skinCount];
        }
        return skins[skinInPreview];
    }

    public InventoryItem GetPreviousSkin()
    {
        skinInPreview = skinInPreview - 1;
        int skinCount = skins.Length - 1;
        if (skinInPreview > skinCount)
        {
            skinInPreview = 0;
            return skins[0];
        }
        else if (skinInPreview < 0)
        {
            skinInPreview = skinCount;
            return skins[skinCount];
        }
        return skins[skinInPreview];
    }

    public void UseSkinFromCollectionByName(string name)
    {
        foreach (InventoryItem item in skins)
        {
            Player player = item.skin.GetComponent<Player>();
            if (player.playerName.ToLower().Equals(name.ToLower()))
            {
                item.inUse = true;
            }
            else
            {
                item.inUse = false;
            }
        }
    }

    public InventoryItem GetSkinByName(string skin)
    {
        InventoryItem itemInUse = null;
        foreach (InventoryItem item in skins)
        {
            Player player = item.skin.GetComponent<Player>();
            if (player.playerName.ToLower().Equals(skin.ToLower()))
            {
                itemInUse = item;
            }
        }
        return itemInUse;
    }

    public InventoryItem GetSkinInUse()
    {
        return null;
    }

    public InventoryItem[] GetSkins()
    {
        return skins;
    }
}

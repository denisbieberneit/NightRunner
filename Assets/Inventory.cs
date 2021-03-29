using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private InventoryItem[] skins;
    public Transform spawn;
    public int skinInPreview;

    public void Awake()
    {
        skins = FindObjectsOfType<InventoryItem>();
        spawn = FindObjectOfType<Spawn>().getTransform();
        int i = 0;
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
            if (player.playerName.Equals(name))
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

    public void UseSkinFromCollectionByName(string name)
    {
        foreach (InventoryItem item in skins)
        {
            Player player = item.skin.GetComponent<Player>();
            if (player.playerName.Equals(name))
            {
                item.inUse = true;
            }
            else
            {
                item.inUse = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<InventoryItem> playerSkinsOwned;
    public Transform spawn;

    private void Awake()
    {
        foreach (InventoryItem item in playerSkinsOwned)
        {
            if (item.inUse)
            {
                UseSkinFromCollection(item.skin.GetComponent<Player>().playerName);
            }
        }
    }

    public void AddSkinToCollection(InventoryItem item)
    {
        playerSkinsOwned.Add(item);
    }

    public void RemoveSkinFromCollection(InventoryItem item)
    {
        playerSkinsOwned.Remove(item);
    }

    public void UseSkinFromCollection(string name)
    {
        Destroy(GameObject.Find("Player"));
        foreach (InventoryItem item in playerSkinsOwned)
        {
            Player player = item.skin.GetComponent<Player>();
            if (player.playerName.Equals(name))
            {
                Instantiate(item.skin,spawn);
                break;
            }
        }
        
    }

}

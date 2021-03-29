using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] Text skinName;
    [SerializeField] Text levelName;
    [SerializeField] GameObject player;
    
    private InventoryItem[] inventoryItems;


    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = inventory.GetComponentsInChildren<InventoryItem>();
        LoadSkinInUse();

        foreach (InventoryItem i in inventoryItems)
        {
            if (i.inUse)
            {
                skinName.text = i.skin.GetComponent<Player>().playerName;
                break;
            }
        }
        
        AudioManager.instance.Play("MainTheme");
    }


    public void OnButtonShop()
    {
        shopCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }
    public void OnButtonMenu()
    {
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void LoadSkinInUse()
    {
        InventoryItem item = null;
        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            if (inventoryItem.inUse)
            {
                item = inventoryItem;
            }
        }
        if (item != null)
        {
            player.GetComponentInChildren<Animator>().runtimeAnimatorController = item.skin.GetComponent<Animator>().runtimeAnimatorController;
            player.GetComponentInChildren<SpriteRenderer>().sprite = item.skin.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Debug.Log("Error while loading player");
        }
    }

    public void getNextSKin()
    {
        InventoryItem item = inventory.GetComponentInChildren<Inventory>().GetNextSkin();
        if (item != null)
        {
            player.GetComponentInChildren<Animator>().runtimeAnimatorController = item.skin.GetComponent<Animator>().runtimeAnimatorController;
            player.GetComponentInChildren<SpriteRenderer>().sprite = item.skin.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Debug.Log("Error while loading player");
        }
        skinName.text = item.skin.GetComponent<Player>().playerName;
    }

    public void UseSkin()
    {
        string skin = skinName.text;

        inventory.GetComponentInChildren<Inventory>().UseSkinFromCollectionByName(skin);
    }
}

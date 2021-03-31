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
    [SerializeField] Text coinsAvailable;
    [SerializeField] Image buySelectButton;
    [SerializeField] GameObject player;
    [SerializeField] Text buyButtonText;

    public Sprite selectSprite;
    public Sprite buySprite;
    
    private InventoryItem[] inventoryItems;

    private string selectedSkinName;


    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = inventory.GetComponentsInChildren<InventoryItem>();
        LoadSkinInUse();

        foreach (InventoryItem i in inventoryItems)
        {
            if (i.inUse)
            {
                selectedSkinName = i.skin.GetComponent<Player>().playerName;
                break;
            }
        }
        
        AudioManager.instance.Play("MainTheme");
    }
    private void Update()
    {
        coinsAvailable.text = ""+PlayerPrefs.GetInt("PlayerCoins");
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
        if (item.owned)
        {
            buySelectButton.sprite = selectSprite;
        }
        else
        {
            buySelectButton.sprite = buySprite;
        }
    }

    public void getPreviousSkin()
    {
        InventoryItem item = inventory.GetComponentInChildren<Inventory>().GetPreviousSkin();
        if (item != null)
        {
            player.GetComponentInChildren<Animator>().runtimeAnimatorController = item.skin.GetComponent<Animator>().runtimeAnimatorController;
            player.GetComponentInChildren<SpriteRenderer>().sprite = item.skin.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Debug.Log("Error while loading player");
        }
        if (item.owned)
        {
            buySelectButton.sprite = selectSprite;
        }
        else
        {
            buySelectButton.sprite = buySprite;
        }
    }

    public void UseSkin()
    {
        InventoryItem itemChoosen = inventory.GetComponentInChildren<Inventory>().GetSkinByName(selectedSkinName);
        if (!itemChoosen.owned)
        {
            int coins = PlayerPrefs.GetInt("PlayerCoins");
            if (coins >= itemChoosen.coinCost)
            {
                itemChoosen.owned = true;
                PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") - itemChoosen.coinCost);
                PlayerPrefs.SetString("PlayerSkin", selectedSkinName);
                inventory.GetComponentInChildren<Inventory>().UseSkinFromCollectionByName(selectedSkinName);
                buySelectButton.sprite = selectSprite;
                SaveSystem.SaveInventory(inventory.GetComponentInChildren<Inventory>());
            }
            else
            {
                //not enough moneyyy
            }
        }
        else
        {
            PlayerPrefs.SetString("PlayerSkin", selectedSkinName);
            inventory.GetComponentInChildren<Inventory>().UseSkinFromCollectionByName(selectedSkinName);
        }
    }

    public void OnButtonSkin()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedSkinName = button.GetComponentInChildren<Text>().text.Trim();
        InventoryItem item = inventory.GetComponentInChildren<Inventory>().GetSkinByName(selectedSkinName);

        if (item != null)
        {
            player.GetComponentInChildren<Animator>().runtimeAnimatorController = item.skin.GetComponent<Animator>().runtimeAnimatorController;
            player.GetComponentInChildren<SpriteRenderer>().sprite = item.skin.GetComponent<SpriteRenderer>().sprite;
            if (item.owned)
            {
                buySelectButton.sprite = selectSprite;
                buyButtonText.text = "SELECT";
            }
            else
            {
                buySelectButton.sprite = buySprite;
                buyButtonText.text = "BUY FOR " + item.coinCost;
            }
        }
        else
        {
            Debug.Log("Could not find skin");
        }
    }
}

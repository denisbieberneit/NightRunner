using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsInShop;
    [SerializeField] Sprite selectedCard;
    [SerializeField] Sprite unselectedCard;
    [SerializeField] Image buySelectButton;

    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject menuCanvas;

    [SerializeField] GameObject player;

    [SerializeField] Text coinsAvailable;
    [SerializeField] Text buyButtonText;
    [SerializeField] GameObject inventory;

    public Sprite selectSprite;
    public Sprite buySprite;


    private string selectedSkinName;
    private string selectedLevelName;
    private InventoryItem[] inventoryItems;

    void Start()
    {
        inventoryItems = inventory.GetComponentsInChildren<InventoryItem>();
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
    private void FixedUpdate()
    {
        coinsAvailable.text = "" + PlayerPrefs.GetInt("PlayerCoins");
    }

    public void OnShowShopButton()
    {
        shopCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        ReloadImages();
    }

    void ReloadImages()
    {
        foreach(GameObject item in itemsInShop)
        {
            item.GetComponent<Image>().sprite = unselectedCard;
        }
    }

    public void OnSelectSkinButton()
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
                buyButtonText.text = "SELECT";
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

    public void OnSkinCardButton()
    {
        ReloadImages();
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedSkinName = button.GetComponentInChildren<Text>().text.Trim();
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = selectedCard;
        InventoryItem item = inventory.GetComponentInChildren<Inventory>().GetSkinByName(selectedSkinName);

        if (item != null)
        {
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

    public void OnLevelCardButton()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedLevelName = button.GetComponentInChildren<Text>().text.Trim();
        PlayerPrefs.SetString("SelectedLevel", selectedLevelName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    // Lists of item data for the store inventory.
    public List<ItemSO> storeInventory = new List<ItemSO>();

    // References to different prefabs for each item type.
    public GameObject itemPrefab;
    public GameObject themePrefab;
    public GameObject soundPrefab;

    // References to different panels for each item type.
    public Transform itemPanel;
    public Transform themePanel;
    public Transform soundPanel;

    // The currently selected theme filter.
    public ThemeType selectedTheme;

    // Reference to the ShopCart script.
    public ShopCart shopCart;

    // This method is called when the script instance is being loaded.
    void Start()
    {
        // Calls the method to populate the store with items when the game starts.
        PopulateStore();
    }

    // This method populates the store UI with items from the store inventory.
    void PopulateStore()
    {
        // Clear existing items in the panels
        foreach (Transform child in itemPanel) { Destroy(child.gameObject); }
        foreach (Transform child in themePanel) { Destroy(child.gameObject); }
        foreach (Transform child in soundPanel) { Destroy(child.gameObject); }

        // Loop through each item in the store inventory.
        foreach (var itemSO in storeInventory)
        {
            // Only populate items that match the selected theme
            if (itemSO.themeType != selectedTheme)
                continue;

            // Determine which prefab to use and which panel to add the item to based on the item type.
            GameObject prefabToUse = null;
            Transform panelToUse = null;
            switch (itemSO.itemType)
            {
                case ItemType.Item:
                    prefabToUse = itemPrefab;
                    panelToUse = itemPanel;
                    break;
                case ItemType.Theme:
                    prefabToUse = themePrefab;
                    panelToUse = themePanel;
                    break;
                case ItemType.Sound:
                    prefabToUse = soundPrefab;
                    panelToUse = soundPanel;
                    break;
            }

            // Instantiate the appropriate prefab and set its parent to the correct panel.
            if (prefabToUse != null && panelToUse != null)
            {
                GameObject slot = Instantiate(prefabToUse, panelToUse);
                ItemUiDisplay itemUiDisplay = slot.GetComponent<ItemUiDisplay>();

                // Check if the ItemUiDisplay component is found on the item slot.
                if (itemUiDisplay != null)
                {
                    // Assign the itemSO (item data) to the itemUiDisplay's item field.
                    itemUiDisplay.item = itemSO;

                    // Add a button click listener to add the item to the cart.
                    slot.GetComponent<Button>().onClick.AddListener(() => AddItemToCart(itemSO));

                    // The Start method in the ItemUiDisplay script will automatically update the UI elements.
                }
                else
                {
                    // Log an error message if the ItemUiDisplay component is not found on the instantiated slot.
                    Debug.LogError("ItemUiDisplay component not found on the instantiated slot.");
                }
            }
            else
            {
                Debug.LogError("No prefab or panel assigned for item type: " + itemSO.itemType);
            }
        }
    }

    // Method to change the theme filter and repopulate the store
    public void ChangeThemeFilter(ThemeType newTheme)
    {
        selectedTheme = newTheme;
        PopulateStore();
    }

    // Method to add an item to the shop cart.
    void AddItemToCart(ItemSO item)
    {
        if (!item.isPurchased && shopCart != null)
        {
            shopCart.AddToCart(item);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public List<ItemSO> storeInventory = new List<ItemSO>();
    public ThemeType selectedTheme;

    public GameObject itemPrefab;
    public GameObject themePrefab;
    public GameObject soundPrefab;

    public Transform itemPanel;
    public Transform themePanel;
    public Transform soundPanel;

    public ShopCart shopCart;
    public ItemManager itemManager;

    void Start()
    {
        PopulateStore();
    }

    void PopulateStore()
    {
        // Clear existing items in the panels
        foreach (Transform child in itemPanel) { Destroy(child.gameObject); }
        foreach (Transform child in themePanel) { Destroy(child.gameObject); }
        foreach (Transform child in soundPanel) { Destroy(child.gameObject); }

        foreach (var itemSO in storeInventory)
        {
            if (itemSO.themeType != selectedTheme)
                continue;

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

            if (prefabToUse != null && panelToUse != null)
            {
                GameObject slot = Instantiate(prefabToUse, panelToUse);
                ItemUiDisplay itemUiDisplay = slot.GetComponent<ItemUiDisplay>();

                if (itemUiDisplay != null)
                {
                    itemUiDisplay.item = itemSO;

                    slot.GetComponent<Button>().onClick.AddListener(() => 
                    {
                        AddItemToCart(itemSO);

                        if (itemManager != null)
                        {
                            itemManager.ToggleSelection(itemSO);
                        }
                    });
                }
                else
                {
                    Debug.LogError("ItemUiDisplay component not found on the instantiated slot.");
                }
            }
            else
            {
                Debug.LogError("No prefab or panel assigned for item type: " + itemSO.itemType);
            }
        }
    }

    public void ChangeThemeFilter(ThemeType newTheme)
    {
        selectedTheme = newTheme;
        PopulateStore();
    }

    void AddItemToCart(ItemSO item)
    {
        if (!item.isPurchased && shopCart != null)
        {
            shopCart.AddToCart(item);
        }
    }

    /// <summary>
    /// Closes the store and handles preview and selected items accordingly.
    /// </summary>
    public void CloseStore()
    {
        if (itemManager != null)
        {
            itemManager.CloseStore();
        }
    }

    /// <summary>
    /// Purchases the items and clears the preview lists.
    /// </summary>
    public void PurchaseItems()
    {
        if (itemManager != null)
        {
            itemManager.PurchaseItems();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpatialSys.UnitySDK;

public class BuyButtonUi : MonoBehaviour
{
    // Reference to the ShopCart script.
    public ShopCart shopCart;

    // Reference to the "Buy" button and its image component.
    public Button buyButton;
    public Image buyButtonImage;

    // Reference to the sprite for the active state of the "Buy" button.
    public Sprite activeBuyButtonSprite;
    // Reference to the sprite for the inactive state of the "Buy" button.
    public Sprite inactiveBuyButtonSprite;

    // Reference to the Text component to display the item count.
    public TMP_Text itemCountText;

    void Start()
    {
        // Subscribe to the onCartUpdated event.
        shopCart.onCartUpdated.AddListener(UpdateUI);
        // Initial UI update.
        UpdateUI();

        // Add a listener to the buy button.
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    void UpdateUI()
    {
        // Update the item count display.
        int itemCount = shopCart.cartItems.Count;
        itemCountText.text = itemCount.ToString();

        // Update the "Buy" button image based on the item count.
        if (itemCount > 0)
        {
            buyButtonImage.sprite = activeBuyButtonSprite;
            buyButton.interactable = true;
        }
        else
        {
            buyButtonImage.sprite = inactiveBuyButtonSprite;
            buyButton.interactable = false;
        }
    }

    void OnBuyButtonClick()
    {
        // Trigger purchase sequence for each item in the cart.
        foreach (ItemSO item in shopCart.cartItems)
        {
            // Call the toolkit's PurchaseItem method.
            PurchaseItem(item.itemID, 1, false);
        }

        // Clear the cart after purchase.
        shopCart.ClearCart();
    }

    void PurchaseItem(string itemID, ulong amount = 1, bool silent = false)
    {

        Debug.Log("Purchased item with ID: " + itemID);
        SpatialBridge.marketplaceService.PurchaseItem(itemID);
        new PurchaseItemRequest();
    }
    
}

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
        }
        else
        {
            buyButtonImage.sprite = inactiveBuyButtonSprite;
        }
    }
}

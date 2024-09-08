using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpatialSys.UnitySDK;

public class BuyButton2 : MonoBehaviour
{
    public ItemManager2 ItemManager2; 
    public TMP_Text ShopCartCountText; 
    public Button BuyButton;
    public Animator ButtonAnimator;
    
    public bool HighlightBuyButtonUI;
    public bool ButtonInteractible;

    void Start()
    {
        if (ItemManager2 == null)
        {
            Debug.LogError("ItemManager2 is not assigned!");
            return;
        }

        BuyButton.onClick.AddListener(OnBuyButtonClick);
    }

    void Update()
    {
        int CartCount = (ItemManager2.ShopCartPreviewSoundsList.Count + ItemManager2.ShopCartPreviewThemesList.Count + ItemManager2.ShopCartPreviewItemsList.Count);
        ShopCartCountText.text = CartCount.ToString(); 

         if (CartCount > 0 && !HighlightBuyButtonUI)
         {
            UpdateBuyButtonUI(); 
            HighlightBuyButtonUI = true;
            ButtonInteractible = true;
        }
         else if (CartCount == 0 && HighlightBuyButtonUI)
        {
        ResetBuyButtonUI();
        }
    }

    void UpdateBuyButtonUI()
    {
        ButtonAnimator.SetTrigger("ItemsinCart");
    }

    void ResetBuyButtonUI()
    {
        if (HighlightBuyButtonUI)  
        {
        HighlightBuyButtonUI = false;
        ButtonInteractible = false;
        ButtonAnimator.ResetTrigger("ItemsinCart");
        }
    }

    void OnBuyButtonClick()
    {
        if (ButtonInteractible)
        {
            Debug.LogError("Buy Button Clicked!");

            foreach (SOsounds sound in ItemManager2.ShopCartPreviewSoundsList)
            {
                SpatialBridge.marketplaceService.PurchaseItem(sound.itemID);
            }

            foreach (SOthemes theme in ItemManager2.ShopCartPreviewThemesList)
            {
                SpatialBridge.marketplaceService.PurchaseItem(theme.itemID);
            }

            // Clear the cart and reset the UI
            //ClearCart();
        }
        else
        {
            Debug.LogError("No Items in Cart");
        }
    }

    void ClearCart()
    {
        ItemManager2.ShopCartPreviewSoundsList.Clear();
        ItemManager2.ShopCartPreviewThemesList.Clear();
        ResetBuyButtonUI();
    }
}

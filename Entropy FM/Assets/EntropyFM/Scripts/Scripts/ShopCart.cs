using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopCart : MonoBehaviour
{
    // List to hold the items added to the shop cart.
    public List<ItemSO> cartItems = new List<ItemSO>();

    // Limits for each item type.
    [SerializeField] private int maxThemes = 1;
    [SerializeField] private int maxItems = 3;
    [SerializeField] private int maxSounds = 1;

    // Indices to track replacement for items.
    private int currentItemIndex = 0;

    // Event to notify when the cart is updated.
    public UnityEvent onCartUpdated = new UnityEvent();

    // Method to add or remove an item from the cart.
    public void AddToCart(ItemSO item)
    {
        // Check if the item is already purchased
        if (item.isPurchased)
        {
            Debug.Log(item.name + " has already been purchased and cannot be added to the cart.");
            return;
        }

        if (cartItems.Contains(item))
        {
            // If the item is already in the cart, remove it.
            RemoveFromCart(item);
        }
        else
        {
            // Otherwise, add the item to the cart.
            switch (item.itemType)
            {
                case ItemType.Theme:
                    HandleTheme(item);
                    break;
                case ItemType.Item:
                    HandleItem(item);
                    break;
                case ItemType.Sound:
                    HandleSound(item);
                    break;
            }
        }
        // Notify listeners that the cart has been updated.
        onCartUpdated.Invoke();
    }

    private void HandleTheme(ItemSO item)
    {
        // Ensure only maxThemes number of themes are in the cart.
        int themeCount = cartItems.FindAll(i => i.itemType == ItemType.Theme).Count;
        if (themeCount >= maxThemes)
        {
            // Replace the existing theme.
            ItemSO existingTheme = cartItems.Find(i => i.itemType == ItemType.Theme);
            cartItems.Remove(existingTheme);
        }
        cartItems.Add(item);
        Debug.Log(item.name + " theme has been added to the cart.");
    }

    private void HandleItem(ItemSO item)
    {
        // Ensure only maxItems number of items are in the cart.
        int itemCount = cartItems.FindAll(i => i.itemType == ItemType.Item).Count;
        if (itemCount >= maxItems)
        {
            // Replace item based on current index.
            List<ItemSO> items = cartItems.FindAll(i => i.itemType == ItemType.Item);
            cartItems.Remove(items[currentItemIndex % maxItems]);
            cartItems.Add(item);
            currentItemIndex++;
        }
        else
        {
            cartItems.Add(item);
        }
        Debug.Log(item.name + " item has been added to the cart.");
    }

    private void HandleSound(ItemSO item)
    {
        // Ensure only maxSounds number of sounds are in the cart.
        int soundCount = cartItems.FindAll(i => i.itemType == ItemType.Sound).Count;
        if (soundCount >= maxSounds)
        {
            // Replace the existing sound.
            ItemSO existingSound = cartItems.Find(i => i.itemType == ItemType.Sound);
            cartItems.Remove(existingSound);
        }
        cartItems.Add(item);
        Debug.Log(item.name + " sound has been added to the cart.");
    }

    // Method to remove an item from the cart.
    public void RemoveFromCart(ItemSO item)
    {
        if (cartItems.Contains(item))
        {
            cartItems.Remove(item);
            Debug.Log(item.name + " has been removed from the cart.");
            onCartUpdated.Invoke();
        }
        else
        {
            Debug.Log(item.name + " is not in the cart.");
        }
    }

    // Method to clear the cart.
    public void ClearCart()
    {
        cartItems.Clear();
        Debug.Log("Cart has been cleared.");
        onCartUpdated.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // Lists for selected items, themes, and sounds
    public List<ItemSO> selectedItems = new List<ItemSO>();
    public List<ItemSO> selectedThemes = new List<ItemSO>();
    public List<ItemSO> selectedSounds = new List<ItemSO>();

    // Properties for the selected sound and theme items
    public ItemSO selectedSound { get; private set; }
    public ItemSO selectedTheme { get; private set; }

    // List for item previews
    public List<ItemSO> itemPreviewSave = new List<ItemSO>();

    // Item zones in the 3D scene
    public Transform[] itemZones;
    private Dictionary<ItemSO, GameObject> itemInstances = new Dictionary<ItemSO, GameObject>();

    // UI Panels for Sound Filter Sprite animation
    public GameObject[] soundFilterPanels;

    // Coroutine to handle sprite animation
    private Coroutine spriteAnimationCoroutine;

    void Start()
    {
        Debug.Log("ItemManager started.");
        SetSoundFilterPanelsAlpha(0); // Initialize panels with alpha 0
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Toggles the selection of an item.
    /// </summary>
    /// <param name="item">The item to toggle.</param>
    public void ToggleSelection(ItemSO item)
    {
        int totalSelectedCount = selectedItems.Count + selectedThemes.Count + selectedSounds.Count;

        if (totalSelectedCount >= 3 && !IsItemSelected(item))
        {
            Debug.LogWarning("Cannot add more than 3 items to the selected lists.");
            return;
        }

        if (item.isPurchased)
        {
            HandlePurchasedItem(item);
        }
        else
        {
            HandlePreviewItem(item);
        }

        // Check and update the alpha of the sound filter panels
        UpdateSoundFilterPanelsAlpha();
    }

    /// <summary>
    /// Handles the selection of a purchased item.
    /// </summary>
    /// <param name="item">The item to handle.</param>
    private void HandlePurchasedItem(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.Item:
                ToggleListItem(selectedItems, item);
                break;
            case ItemType.Sound:
                ToggleListItem(selectedSounds, item, true);
                PlaySoundFilterSprite(item);
                break;
            case ItemType.Theme:
                ToggleListItem(selectedThemes, item, true);
                break;
        }
    }

    /// <summary>
    /// Handles the selection of a preview item.
    /// </summary>
    /// <param name="item">The item to handle.</param>
    private void HandlePreviewItem(ItemSO item)
    {
        if (selectedItems.Contains(item) || selectedSounds.Contains(item) || selectedThemes.Contains(item))
        {
            RemoveFromSelectedList(item);
            AddItemToPreview(item);
        }
        else
        {
            switch (item.itemType)
            {
                case ItemType.Item:
                    if (selectedItems.Count < 3)
                    {
                        selectedItems.Add(item);
                        PlaceItemInScene(item);
                    }
                    else
                    {
                        ItemSO lastSelectedItem = selectedItems[selectedItems.Count - 1];
                        selectedItems.RemoveAt(selectedItems.Count - 1);
                        AddItemToPreview(lastSelectedItem);

                        selectedItems.Add(item);
                        PlaceItemInScene(item);
                    }
                    break;
                case ItemType.Sound:
                    MoveToSelectedList(selectedSounds, item, true);
                    PlaySoundFilterSprite(item);
                    break;
                case ItemType.Theme:
                    MoveToSelectedList(selectedThemes, item, true);
                    break;
            }
        }
    }

    /// <summary>
    /// Adds an item to the preview list and removes it from the scene.
    /// </summary>
    /// <param name="item">The item to add.</param>
    private void AddItemToPreview(ItemSO item)
    {
        itemPreviewSave.Add(item);
        RemoveItemFromScene(item);
    }

    /// <summary>
    /// Places an item in the first available item zone in the 3D scene.
    /// </summary>
    /// <param name="item">The item to place.</param>
    private void PlaceItemInScene(ItemSO item)
    {
        if (item.model == null)
        {
            Debug.LogWarning("Model is not assigned for item: " + item.name);
            return;
        }

        for (int i = 0; i < itemZones.Length; i++)
        {
            if (itemZones[i].childCount == 0)
            {
                GameObject itemInstance = Instantiate(item.model, itemZones[i]);
                itemInstances[item] = itemInstance;
                break;
            }
        }
    }

    /// <summary>
    /// Removes an item from the 3D scene and updates the itemInstances dictionary.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    private void RemoveItemFromScene(ItemSO item)
    {
        if (itemInstances.ContainsKey(item))
        {
            Destroy(itemInstances[item]);
            itemInstances.Remove(item);
        }
        RepositionItemsInScene();
    }

    /// <summary>
    /// Repositions items in the 3D scene to fill any gaps left by removed items.
    /// </summary>
    private void RepositionItemsInScene()
    {
        int index = 0;
        foreach (var selectedItem in selectedItems)
        {
            if (itemInstances.ContainsKey(selectedItem))
            {
                itemInstances[selectedItem].transform.SetParent(itemZones[index], false);
                itemInstances[selectedItem].transform.localPosition = Vector3.zero;
            }
            index++;
        }
    }

    /// <summary>
    /// Toggles an item in the provided list.
    /// </summary>
    /// <param name="list">The list to toggle the item in.</param>
    /// <param name="item">The item to toggle.</param>
    /// <param name="singleItemAllowed">Whether only a single item is allowed in the list.</param>
    private void ToggleListItem(List<ItemSO> list, ItemSO item, bool singleItemAllowed = false)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            RemoveItemFromScene(item);
            if (item.itemType == ItemType.Sound)
            {
                if (spriteAnimationCoroutine != null)
                {
                    StopCoroutine(spriteAnimationCoroutine);
                    ResetSoundFilterPanels();
                }
            }
        }
        else
        {
            if (singleItemAllowed && list.Count > 0)
            {
                ItemSO lastSelectedItem = list[0];
                list.RemoveAt(0);
                AddItemToPreview(lastSelectedItem);
            }
            list.Add(item);
            PlaceItemInScene(item);
        }
    }

    /// <summary>
    /// Moves an item to the selected list and handles preview and scene placement.
    /// </summary>
    /// <param name="list">The selected list to move the item to.</param>
    /// <param name="item">The item to move.</param>
    /// <param name="singleItemAllowed">Whether only a single item is allowed in the list.</param>
    private void MoveToSelectedList(List<ItemSO> list, ItemSO item, bool singleItemAllowed = false)
    {
        if (singleItemAllowed && list.Count > 0)
        {
            ItemSO lastSelectedItem = list[0];
            list.RemoveAt(0);
            AddItemToPreview(lastSelectedItem);
        }
        list.Add(item);
        PlaceItemInScene(item);
    }

    /// <summary>
    /// Removes an item from the selected lists.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    private void RemoveFromSelectedList(ItemSO item)
    {
        selectedItems.Remove(item);
        selectedSounds.Remove(item);
        selectedThemes.Remove(item);
        RemoveItemFromScene(item);
    }

    /// <summary>
    /// Closes the store and handles preview and selected items accordingly.
    /// </summary>
    public void CloseStore()
    {
        foreach (var item in selectedItems)
        {
            if (!item.isPurchased)
            {
                AddItemToPreview(item);
            }
        }
        selectedItems.Clear();

        foreach (var item in itemPreviewSave)
        {
            selectedItems.Add(item);
            PlaceItemInScene(item);
        }

        itemPreviewSave.Clear();
    }

    /// <summary>
    /// Purchases the items and clears the preview lists.
    /// </summary>
    public void PurchaseItems()
    {
        itemPreviewSave.Clear();
    }

    /// <summary>
    /// Checks if an item is already selected.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>True if the item is already selected, false otherwise.</returns>
    private bool IsItemSelected(ItemSO item)
    {
        return selectedItems.Contains(item) || selectedSounds.Contains(item) || selectedThemes.Contains(item);
    }

    /// <summary>
    /// Plays the sound filter sprite animation.
    /// </summary>
    /// <param name="soundItem">The sound item to play the animation for.</param>
    private void PlaySoundFilterSprite(ItemSO soundItem)
    {
        Debug.Log("PlaySoundFilterSprite called for item: " + soundItem.name);
        
        if (soundItem == null || soundItem.SoundFilterSprite == null || soundFilterPanels == null)
        {
            Debug.LogError("Sound item or its sprites or soundFilterPanels are null.");
            SetSoundFilterPanelsAlpha(0); // Set alpha to 0 if no sprites
            return;
        }

        SetSoundFilterPanelsAlpha(255); // Set alpha to 255 if sprites are available

        if (spriteAnimationCoroutine != null)
        {
            StopCoroutine(spriteAnimationCoroutine);
        }

        spriteAnimationCoroutine = StartCoroutine(PlaySoundFilterSpriteAnimation(soundItem));
    }

    /// <summary>
    /// Coroutine to play the sound filter sprite animation.
    /// </summary>
    /// <param name="soundItem">The sound item to play the animation for.</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlaySoundFilterSpriteAnimation(ItemSO soundItem)
    {
        int spriteIndex = 0;
        float delay = 1f / soundItem.SoundFilterSpeed;

        while (true)
        {
            Debug.Log("Animating sprite index: " + spriteIndex);

            foreach (GameObject panel in soundFilterPanels)
            {
                Image panelImage = panel.GetComponent<Image>();
                if (panelImage != null)
                {
                    panelImage.sprite = soundItem.SoundFilterSprite[spriteIndex];
                    panelImage.color = new Color(soundItem.SoundFilterOverlayColor.r, soundItem.SoundFilterOverlayColor.g, soundItem.SoundFilterOverlayColor.b, panelImage.color.a); // Apply overlay color with existing alpha
                }
                else
                {
                    Debug.LogError("Panel does not have an Image component: " + panel.name);
                }
            }

            spriteIndex = (spriteIndex + 1) % soundItem.SoundFilterSprite.Length;
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// Updates the alpha of the sound filter panels based on the selected sounds list.
    /// </summary>
    private void UpdateSoundFilterPanelsAlpha()
    {
        bool hasSelectedSounds = selectedSounds.Count > 0;
        SetSoundFilterPanelsAlpha(hasSelectedSounds ? 255 : 0);
    }

    /// <summary>
    /// Sets the alpha of the sound filter panels.
    /// </summary>
    /// <param name="alpha">The alpha value to set.</param>
    private void SetSoundFilterPanelsAlpha(float alpha)
    {
        foreach (GameObject panel in soundFilterPanels)
        {
            Image panelImage = panel.GetComponent<Image>();
            if (panelImage != null)
            {
                Color color = panelImage.color;
                color.a = alpha / 255f;
                panelImage.color = color;
            }
        }
    }

    /// <summary>
    /// Resets the sound filter panels by clearing the sprites and setting the alpha to 0.
    /// </summary>
    private void ResetSoundFilterPanels()
    {
        foreach (GameObject panel in soundFilterPanels)
        {
            Image panelImage = panel.GetComponent<Image>();
            if (panelImage != null)
            {
                panelImage.sprite = null;
                panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0); // Set alpha to 0
            }
        }
    }
}

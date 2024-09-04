using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager2 : MonoBehaviour
{
    [Header("General")]
     public ThemeType selectedTheme;
    public KeyCode OpenStoreHotkey = KeyCode.B;
    public Button CloseStoreButton;
    public bool StoreOpen;
    public ItemManager2 itemManager2;
    public Animator StoreAnimationController;
    public Animator MusicPlayerAnimationController;
    public List<SOthemes> StoreThemeInventory = new List<SOthemes>();
    public List<SOitems> StoreItemInventory = new List<SOitems>();
    public List<SOsounds> StoreSoundInventory = new List<SOsounds>();
    
     [Header("Store Prefabs")]
    public GameObject themePrefab;
    public GameObject itemPrefab;
    public GameObject soundPrefab;

    [Header("Store Panels")]
    public Transform themePanel;
    public Transform itemPanel;
    public Transform soundPanel;

    [Header("Scroll Sections")]
    public bool CategoryOneSelected;
    public bool CategoryTwoSelected;
    public bool CategoryThreeSelected;

    public Button CatgoryOneButton;
    public Button CatgoryTwoButton;
    public Button CatgoryThreeButton;
    
    public Animator CategoryOneAnimator;
    public Animator CategoryTwoAnimator;
    public Animator CategoryThreeAnimator;

    public RectTransform storeListRectTransform;

    public float category1PositionY;
    public float category2PositionY;
    public float category3PositionY;

    private Coroutine scrollCoroutine;

    public float scrollSpeed = 10f;

    

    // Start is called before the first frame update
    void Start()
    {
        PopulateThemes();
        PopulateSounds();
        PopulateItems();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHotkeys();

        // Check the current position of the store list and update the category booleans accordingly
        float currentY = storeListRectTransform.anchoredPosition.y;

        if (currentY >= category3PositionY)
        {
            SetCategory(3);
        }
        else if (currentY >= category2PositionY)
        {
            SetCategory(2);
        }
        else if (currentY >= category1PositionY)
        {
            SetCategory(1);
        }
    }

    private void CheckHotkeys()
    {
        if (Input.GetKeyDown(OpenStoreHotkey) && StoreOpen == false)
        {
            OpenStore(); 
        }
        else if (Input.GetKeyDown(OpenStoreHotkey) && StoreOpen == true)
        {
            CloseStore();    
        }
    }

    public void OpenStore()
    {
        StoreOpen = !StoreOpen;
        StoreAnimationController.SetTrigger("Select");
    }

    public void CloseStore()
    {
        StoreAnimationController.SetTrigger("Select");
        StoreOpen = !StoreOpen;
        //Debug.LogError("CloseStore");
    }

    // Method to set the category based on the current scroll position
    private void SetCategory(int categoryNumber)
    {
        // Reset all categories to false
        CategoryOneSelected = false;
        CategoryTwoSelected = false;
        CategoryThreeSelected = false;

        // Set all animators' 'isSelected' parameter to false
        CategoryOneAnimator.SetBool("isSelected", false);
        CategoryTwoAnimator.SetBool("isSelected", false);
        CategoryThreeAnimator.SetBool("isSelected", false);
   

        // Set the selected category to true and its animator's 'isSelected' to true
        switch (categoryNumber)
        {
            case 1:
                CategoryOneSelected = true;
                CategoryOneAnimator.SetBool("isSelected", true);
                break;
            case 2:
                CategoryTwoSelected = true;
                CategoryTwoAnimator.SetBool("isSelected", true);
                break;
            case 3:
                CategoryThreeSelected = true;
                CategoryThreeAnimator.SetBool("isSelected", true);
                break;
            default:
                Debug.LogWarning("Invalid category number");
                break;
        }
    }

    // Call this method when a category button is clicked
    public void SelectCategory(int categoryNumber)
    {
         if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }

        // Start a new coroutine to scroll to the selected category
        scrollCoroutine = StartCoroutine(ScrollToPosition(GetCategoryPositionY(categoryNumber)));
    }

     private IEnumerator ScrollToPosition(float targetY)
    {
         while (Mathf.Abs(storeListRectTransform.anchoredPosition.y - targetY) > 0.1f)
        {
            float newY = Mathf.Lerp(storeListRectTransform.anchoredPosition.y, targetY, scrollSpeed * Time.deltaTime);
            storeListRectTransform.anchoredPosition = new Vector2(storeListRectTransform.anchoredPosition.x, newY);
            yield return null;
        }
        storeListRectTransform.anchoredPosition = new Vector2(storeListRectTransform.anchoredPosition.x, targetY);

        // Set the coroutine reference to null once the scrolling is done
        scrollCoroutine = null;
    }

   // Helper method to get the Y position for a given category
    private float GetCategoryPositionY(int categoryNumber)
    {
        switch (categoryNumber)
        {
            case 1: return category1PositionY;
            case 2: return category2PositionY;
            case 3: return category3PositionY;
            default: return category1PositionY; // Default to category 1 if an invalid number is passed
        }
    }


      void PopulateThemes()
    {
        // Set Gameobject active (helps with layout bug)
        if (themePanel != null){
            themePanel.gameObject.SetActive(true);
        }

        // Check if there are any scriptable objects listed under StoreThemeInventory
        if (StoreThemeInventory != null && StoreThemeInventory.Count > 0)
        {
        // Loop through each scriptable object in StoreThemeInventory
        foreach (var theme in StoreThemeInventory)
        {
            // Instantiate the prefab from the ThemePrefab property
            GameObject instantiatedTheme = Instantiate(themePrefab, themePanel);

            // Access the UIsounds script within the instantiated prefab
            UIthemes UIthemes = instantiatedTheme.GetComponent<UIthemes>();

            // Set the 'theme' property of the UIsounds script to match the listed object
            if (UIthemes != null)
            {
                UIthemes.theme = theme;

                UIthemes.GetComponent<Button>().onClick.AddListener(() => 
                    {
                        if (itemManager2 != null)
                        {
                            SendThemeToItemManager(theme);
                            //Debug.LogError("Click_Theme");
                            
                        }
                        else
                        {
                             Debug.LogError("No ItemManager within StoreManager ");
                        }
                    });
            }

            // Place the instantiated item within the transform provided by the soundPanel property
            instantiatedTheme.transform.SetParent(themePanel, false);
        }
    }

    }

    
    void PopulateItems()
    {

        if (StoreItemInventory != null && StoreItemInventory.Count > 0)
        {
            // Loop through each scriptable object in StoreThemeInventory
            foreach (var item in StoreItemInventory)
            {

            // Instantiate the prefab from the ThemePrefab property
            GameObject instantiatedItem = Instantiate(itemPrefab, itemPanel);

            // Access the UIitems script within the instantiated prefab
            UIitems UIitems = instantiatedItem.GetComponent<UIitems>();

            // Set the 'item' property of the UIsounds script to match the listed object
            if (UIitems != null)
            {
                UIitems.item = item;

                UIitems.GetComponent<Button>().onClick.AddListener(() => 
                    {
                        if (itemManager2 != null)
                        {
                            SendItemToItemManager(item);
                            //Debug.LogError("Click_Theme");
                            
                        }
                        else
                        {
                             Debug.LogError("No ItemManager within StoreManager ");
                        }
                    });
            }

            // Place the instantiated item within the transform provided by the soundPanel property
            instantiatedItem.transform.SetParent(itemPanel, false);

        }


        }

    }


    void PopulateSounds()
    {
      // Check if there are any scriptable objects listed under StoreSoundInventory
    if (StoreSoundInventory != null && StoreSoundInventory.Count > 0)
    {
        // Loop through each scriptable object in StoreSoundInventory
        foreach (var sound in StoreSoundInventory)
        {
            // Instantiate the prefab from the soundPrefab property
            GameObject instantiatedSound = Instantiate(soundPrefab, soundPanel);

            // Access the UIsounds script within the instantiated prefab
            UIsounds UISounds = instantiatedSound.GetComponent<UIsounds>();

            // Set the 'sound' property of the UIsounds script to match the listed object
            if (UISounds != null)
            {
                UISounds.sound = sound;

                 UISounds.GetComponent<Button>().onClick.AddListener(() => 
                    {
                        if (itemManager2 != null)
                        {
                             SendSoundToItemManager(sound);   
                        }
                        else
                        {
                             Debug.LogError("No ItemManager within StoreManager ");
                        }
                    });
            }

            // Place the instantiated item within the transform provided by the soundPanel property
            instantiatedSound.transform.SetParent(soundPanel, false);
        }
    }

    }


    void ThemeToPreviewList(SOthemes theme)
    {
        itemManager2.AddThemeToShopCartPreviewList(theme);
    }


       void SendThemeToItemManager(SOthemes theme)
    {
         if (theme.purchased == false)
            {
              itemManager2.AddThemeToShopCartPreviewList(theme);
            }

        if (theme.purchased == true)
        {
          itemManager2.AddThemeToSelectedList(theme);  
        }
        
    }
    
    void SendSoundToItemManager(SOsounds sound)
    {
         if (sound.purchased == false)
            {
              itemManager2.AddSoundToShopCartPreviewList(sound);
            }

        if (sound.purchased == true)
        {
          itemManager2.AddSoundToSelectedList(sound);  
        }
        
    }

    void SendItemToItemManager(SOitems item)
    {
         if (item.purchased == false)
            {
              itemManager2.AddItemToShopCartPreviewList(item);
            }

        if (item.purchased == true)
        {
          itemManager2.AddItemToSelectedList(item);  
        }
        
    }

    public void ScrollToSection()
    {
        Canvas.ForceUpdateCanvases();
    }

}

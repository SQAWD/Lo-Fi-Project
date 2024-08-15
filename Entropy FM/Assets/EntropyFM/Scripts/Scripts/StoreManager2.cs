using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager2 : MonoBehaviour
{
    [Header("General")]
     public ThemeType selectedTheme;
    public KeyCode OpenStoreHotkey = KeyCode.B;
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
    }

    private void CheckHotkeys()
    {
        if (Input.GetKeyDown(OpenStoreHotkey))
        {
            StoreAnimationController.SetTrigger("Select");
            Debug.Log("OpenStoreHotKey");
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
            foreach (var item in StoreItemInventory)
            {
                GameObject InstantiatedItem = Instantiate (itemPrefab, itemPanel);

                UIitems UIitems = InstantiatedItem.GetComponent<UIitems>();

                if (UIitems != null)
                {
                    UIitems.item = item;
                }
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
        itemManager2.AddThemeToPreviewList(theme);
    }


       void SendThemeToItemManager(SOthemes theme)
    {
         if (theme.purchased == false)
            {
              itemManager2.AddThemeToPreviewList(theme);
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

}

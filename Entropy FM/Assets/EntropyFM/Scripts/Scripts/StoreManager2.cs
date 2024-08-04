using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager2 : MonoBehaviour
{
    [Header("General")]
     public ThemeType selectedTheme;
    public KeyCode OpenStoreHotkey = KeyCode.B;
    public ShopCart shopCart;
    public ItemManager itemManager;
    public Animator StoreAnimationController;
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
        PopulateStore();
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

    void PopulateStore()
    {
         // Instantiate theme prefabs into the theme panel
    foreach (var theme in StoreThemeInventory)
    {
        GameObject themeObject = Instantiate(themePrefab, themePanel);
    }
    }
}

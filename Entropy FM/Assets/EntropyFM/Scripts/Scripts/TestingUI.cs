using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingUI : MonoBehaviour
{

    public Button showItemTargets;
    public bool ItemTargetsEnabled;
    public InventorySlot inventorySlot;
    public ItemManager2 itemManager2;
    public string selectedTheme;
    
    // Start is called before the first frame update
    void Start()
    {
        showItemTargets.onClick.AddListener(EnableItemTargets);
    }

    void EnableItemTargets(){
        ItemTargetsEnabled = !ItemTargetsEnabled;   
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ShowSelectedTheme() {
        itemManager2.currentTheme == selectedTheme ;
    }


}

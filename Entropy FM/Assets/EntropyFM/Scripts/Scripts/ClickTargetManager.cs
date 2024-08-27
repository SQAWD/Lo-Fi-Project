using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTargetManager : MonoBehaviour
{
    public StoreManager2 StoreManager2;
    public Button SoundClickTargetButton;
    public Button ThemeClickTargetButton;

    void Start()
    {   
        SoundClickTargetButton.onClick.AddListener(OnButtonClick);
        ThemeClickTargetButton.onClick.AddListener(OnButtonClick);
    }

     public void OnButtonClick()
    {
        StoreManager2.OpenStore();
    }

    void Update()
    {
        // Handle the visibility of the buttons based on the StoreManager2 state
        if (StoreManager2.StoreOpen)
        {
            SoundClickTargetButton.gameObject.SetActive(false);
            ThemeClickTargetButton.gameObject.SetActive(false);
        }
        else
        {
            ThemeClickTargetButton.gameObject.SetActive(true);
            SoundClickTargetButton.gameObject.SetActive(true);
        }

    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpatialSys.UnitySDK;

public class ClickTargetManager : MonoBehaviour
{
    public StoreManager2 StoreManager2;
    public Button ThemeClickTargetButton;
    public Button SoundClickTargetButton;
    public GameObject SoundClickTargetGameObject;
    public GameObject ThemeClickTargetGameObject;

    void Start()
    {
        
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
            SoundClickTargetButton.gameObject.SetActive(true);
            ThemeClickTargetButton.gameObject.SetActive(true);
        }

    }

}

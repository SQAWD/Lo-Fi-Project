using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpatialSys.UnitySDK;

public class GimmeGems : MonoBehaviour
{
    public Button GemsButton;
    // Start is called before the first frame update
    void Start()
    {
        GemsButton.onClick.AddListener(OnGimmeGemsButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGimmeGemsButtonClick()
    {
        Debug.LogError("Gimme Gems");
        SpatialBridge.inventoryService.AwardWorldCurrency(20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class SCENEMANAGER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpatialBridge.coreGUIService.SetCoreGUIEnabled(SpatialCoreGUITypeFlags.Backpack,false);
        SpatialBridge.coreGUIService.SetCoreGUIEnabled(SpatialCoreGUITypeFlags.UniversalShop,false);
        SpatialBridge.coreGUIService.SetCoreGUIEnabled(SpatialCoreGUITypeFlags.Emote,false);
        SpatialBridge.coreGUIService.SetCoreGUIEnabled(SpatialCoreGUITypeFlags.ParticipantsList,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

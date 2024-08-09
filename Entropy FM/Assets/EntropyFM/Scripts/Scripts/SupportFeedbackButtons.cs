using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
using UnityEngine.UI;

public class SupportFeedbackButtons : MonoBehaviour
{
    
    public Button FeedbackButton;
    public Button SupportButton;
    
    // Start is called before the first frame update
    void Start()
    {
        FeedbackButton.onClick.AddListener(OnFeedbackButtonClick);
        SupportButton.onClick.AddListener(OnSupportButtonClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


 void OnFeedbackButtonClick()
 {
    SpatialBridge.spaceService.OpenURL("https://entropyfm.featurebase.app");
    //Debug.LogError("click_feedback");
 }

 
 void OnSupportButtonClick()
 {
    SpatialBridge.spaceService.OpenURL("https://www.patreon.com/");
   //Debug.LogError("click_Support");
 }

}

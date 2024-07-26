using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpatialSys.UnitySDK;

public class MusicPlayer : MonoBehaviour
{
    public Button AlbumButton;
    public Button ContactUsButton;


    public KeyCode AdvancedOptionsHotkey = KeyCode.Return;
    public Animator AdvancedOptionsController; 
    public GameObject AdvancedOptionsObject;

    public Button MuteButton;
    public KeyCode MusicHotkey = KeyCode.Space;
    public GameObject MutedOverlay;
    public AudioSource MusicAudioSource;
    public float MusicVolume;
    public int MusicSlider = 5;
    public bool isMusicMuted;
    
    public Button SoundButton;
    public float SoundVolume;
    public int SoundSlider;
    public bool isSoundMuted;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button to call OnButtonClick when clicked
        MuteButton.onClick.AddListener(ButtonPressed_Music);
        AlbumButton.onClick.AddListener(ButtonPressed_Album);
        ContactUsButton.onClick.AddListener(ButtonPressed_ContactUs);
        AdvancedOptionsController = AdvancedOptionsObject.GetComponent<Animator>();

    }

    void ButtonPressed_Music()
      {
        ToggleMute();
      }   

      void ButtonPressed_MusicArrows()
      {
        
      }   

    void ButtonPressed_ContactUs()
      {
        SpatialBridge.spaceService.OpenURL("https://www.typeform.com/product-overview/");
      }   

    void Update()
    {
        // Check if the hotkey is pressed
        if (Input.GetKeyDown(MusicHotkey))
        {
            ToggleMute();
        }

         if (Input.GetKeyDown(AdvancedOptionsHotkey))
        {
            AdvancedOptionsController.SetTrigger("Select");
            Debug.Log("OpenAdvancedOptions");
        }
    }
    
    void ButtonPressed_Album()
    {
        SpatialBridge.spaceService.OpenURL("https://open.spotify.com/user/31x4nhmpfywcn32e4dgtgoczcway");
    }


    void ToggleMute()
    {
        isMusicMuted = !isMusicMuted;

         if (isMusicMuted)
        {
            MusicAudioSource.volume = 0;
            MutedOverlay.SetActive(true);
        }
        else
        {
            MusicAudioSource.volume = 1;
            MutedOverlay.SetActive(false);
        }
    }


    

}

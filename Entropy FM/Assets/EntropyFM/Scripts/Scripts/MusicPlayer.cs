using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpatialSys.UnitySDK;
using System.Xml.Serialization;

public class MusicPlayer : MonoBehaviour
{
    [Header("Buttons")]
    public Button AlbumButton;
    public Button ContactUsButton;
    public Button MusicIncreaseButton;
    public Button MusicDecreaseButton;
    public Button MuteButton;
    public Button SoundButton;
    public Button SoundIncreaseButton;
    public Button SoundDecreaseButton;

    [Header("Hotkeys")]
    public KeyCode AdvancedOptionsHotkey = KeyCode.Return;
    public KeyCode MusicHotkey = KeyCode.Space;

    [Header("Advanced Options")]
    public Animator AdvancedOptionsController; 
    public GameObject AdvancedOptionsObject;

    [Header("Music Settings")]
    public GameObject MutedOverlay;
    public TextMeshProUGUI MusicSliderNum;
    public AudioSource MusicAudioSource;
    [SerializeField] float MusicVolume;
    [SerializeField] int MusicSlider;
    public bool isMusicMuted;
     private const int minMusicSliderValue = 0;
    private const int maxMusicSliderValue = 5;

    [Header("Sound Settings")]
    public AudioSource SoundAudioSource;
    public TextMeshProUGUI SoundSliderNum;
    public float SoundVolume;
    public int SoundSlider;
    public bool isSoundMuted;
     private const int minSoundSliderValue = 0;
    private const int maxSoundSliderValue = 5;

   

    // Start is called before the first frame update
    void Start()
    {

        InitializeButtons();
        AdvancedOptionsController = AdvancedOptionsObject.GetComponent<Animator>();

        UpdateVolume();
    }

     void Update()
    {
        CheckHotkeys();
        MusicSliderNum.text = MusicSlider.ToString();
        MusicVolume = MusicAudioSource.volume;

        SoundSliderNum.text = SoundSlider.ToString();
        SoundVolume = SoundAudioSource.volume;

    }

    private void InitializeButtons()
    {
        MusicDecreaseButton.onClick.AddListener(DecreaseMusicVolume);
        SoundDecreaseButton.onClick.AddListener(DecreaseSoundVolume);
        MuteButton.onClick.AddListener(ButtonPressed_Music);
        AlbumButton.onClick.AddListener(ButtonPressed_Album);
        ContactUsButton.onClick.AddListener(ButtonPressed_ContactUs);
        MusicIncreaseButton.onClick.AddListener(IncreaseMusicVolume);
        SoundIncreaseButton.onClick.AddListener(IncreaseSoundVolume);
    }

    void ButtonPressed_Music()
    {
        ToggleMusicMute();
    }

    void IncreaseMusicVolume()
    {
        if (MusicSlider < maxMusicSliderValue)
        {
            MusicSlider++;
            UpdateVolume();
        }
    }

    void DecreaseMusicVolume()
    {
        if (MusicSlider > minMusicSliderValue)
        {
            Debug.Log("MusicVolumeDecrease");
            MusicSlider--;
            UpdateVolume();
        }
        
    }

     void IncreaseSoundVolume()
    {
        if (SoundSlider < maxSoundSliderValue)
        {
            SoundSlider++;
            UpdateVolume();
            Debug.Log("SoundVolumeIncrease");
        }
    }

    void DecreaseSoundVolume()
    {
        if (SoundSlider > minSoundSliderValue)
        {
            SoundSlider--;
            UpdateVolume();
            Debug.Log("SoundVolumeDecrease");
        }
        
    }

    void UpdateVolume()
    {
        
        if (isMusicMuted == false)
        {
            MusicAudioSource.volume = 1f / maxMusicSliderValue * MusicSlider;
        }
        else
        {
            MusicAudioSource.volume = 0;
        }

        if (isSoundMuted == false)
        {
            SoundAudioSource.volume = 1f / maxSoundSliderValue * SoundSlider;
        }
        else
        {
            SoundAudioSource.volume = 0;
        }

    }

    void ButtonPressed_ContactUs()
    {
        SpatialBridge.spaceService.OpenURL("https://www.typeform.com/product-overview/");
        Debug.Log("Contact Us Click");
    }

    void ButtonPressed_Album()
    {
        SpatialBridge.spaceService.OpenURL("https://open.spotify.com/user/31x4nhmpfywcn32e4dgtgoczcway");
        Debug.Log("Album Click");
    }

   
    private void CheckHotkeys()
    {
        if (Input.GetKeyDown(MusicHotkey))
        {
            ToggleMusicMute();
        }

        if (Input.GetKeyDown(AdvancedOptionsHotkey))
        {
            AdvancedOptionsController.SetTrigger("Select");
            Debug.Log("OpenAdvancedOptions");
        }
    }

    void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;

        if (isMusicMuted)
        {
            MusicAudioSource.volume = 0;
            MutedOverlay.SetActive(true);
            Debug.Log("IsMuted TRUE");
        }
        else
        {
            MusicAudioSource.volume = 1;
            MutedOverlay.SetActive(false);
            Debug.Log("IsMuted FALSE");
        }
    }
}

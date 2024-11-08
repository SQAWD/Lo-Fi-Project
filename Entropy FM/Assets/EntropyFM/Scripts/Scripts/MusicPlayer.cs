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
    [Header("MusicPlayerManager")]
    public MusicPlayerManager MusicPlayerManager;

    [Header("Buttons")]
    public Button AlbumButton;
    public Button ContactUsButton;
    public Button MusicIncreaseButton;
    public Button MusicDecreaseButton;
    public Button MuteButton;
    public Animator MuteButtonAnimator;
    public Button SoundButton;
    public Animator SoundButtonAnimator;
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
    public Image AlbumCover;
    public TMP_Text ArtistNameText;
    public TMP_Text PlaylistNameText;
    private String AlbumLink;
    public bool isMusicMuted;
     private const int minMusicSliderValue = 0;
    private const int maxMusicSliderValue = 5;

    [Header("Sound Settings")]
    public AudioSource SoundAudioSource;
    public TextMeshProUGUI SoundSliderNum;
    public AudioSource UiAudioSource;
    public float SoundVolume;
    public int SoundSlider;
    public bool isSoundMuted;
     private const int minSoundSliderValue = 0;
    private const int maxSoundSliderValue = 5;

   

    // Start is called before the first frame update
    void Start()
    {

        InitializeButtons();
        UpdateVolume();
        UpdateMusicAudioSource();
        AdvancedOptionsController = AdvancedOptionsObject.GetComponent<Animator>();
    }

     void Update()
    {
        CheckHotkeys();
        MusicSliderNum.text = MusicSlider.ToString();
        MusicVolume = MusicAudioSource.volume;
        SoundSliderNum.text = SoundSlider.ToString();
        SoundVolume = SoundAudioSource.volume;
        
    }

    public void UpdateMusicPlayerUI(PlaylistSO song)
    {
        ArtistNameText.text = song.Artist;
        AlbumCover.sprite = song.AlbumCover;
        AlbumLink = song.AlbumLink;
        PlaylistNameText.text = song.Title;
        Debug.Log("UpdateSongUi");
    }

    void UpdateMusicAudioSource()
    {
        Debug.Log("UpdateMusicAudioSource");
        if (MusicPlayerManager != null && MusicPlayerManager.CurrentPlaylist != null)
    {
        MusicAudioSource.clip = MusicPlayerManager.CurrentPlaylist.AudioFile;
        MusicAudioSource.Play();
    }
    }

    private void InitializeButtons()
    {
        MusicDecreaseButton.onClick.AddListener(DecreaseMusicVolume);
        SoundDecreaseButton.onClick.AddListener(DecreaseSoundVolume);
        MuteButton.onClick.AddListener(ButtonPressed_Music);
        SoundButton.onClick.AddListener(ButtonPressed_Sound);
        AlbumButton.onClick.AddListener(ButtonPressed_Album);
        ContactUsButton.onClick.AddListener(ButtonPressed_ContactUs);
        MusicIncreaseButton.onClick.AddListener(IncreaseMusicVolume);
        SoundIncreaseButton.onClick.AddListener(IncreaseSoundVolume);
    }

    void ButtonPressed_Music()
    {
        ToggleMusicMute();
    }

    void ButtonPressed_Sound()
    {
        ToggleSoundMute();
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
        SpatialBridge.spaceService.OpenURL(AlbumLink);
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

    void ToggleSoundMute()
    {
        isSoundMuted = !isSoundMuted;

        if (isSoundMuted)
        {
            SoundAudioSource.volume = 0;
            Debug.Log("IsSoundMuted TRUE");
        }
        else
        {
            SoundAudioSource.volume = 1f / maxSoundSliderValue * SoundSlider;
            Debug.Log("IsMuted FALSE");
        }
    }

    void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;

        if (isMusicMuted)
        {
            MuteButtonAnimator.SetTrigger("Selected");
            SoundButtonAnimator.SetTrigger("Selected");
            MusicAudioSource.volume = 0;
            SoundAudioSource.volume = 0;
            MutedOverlay.SetActive(true);
            UiAudioSource.Play();
            //Debug.Log("IsMuted TRUE");
        }
        else
        {
            MuteButtonAnimator.SetTrigger("Selected");
            SoundButtonAnimator.SetTrigger("Selected");
            SoundAudioSource.volume = 1f / maxSoundSliderValue * SoundSlider;
            MusicAudioSource.volume = 1f / maxMusicSliderValue * MusicSlider;;
            MutedOverlay.SetActive(false);
            //Debug.Log("IsMuted FALSE");
        }
    }
}

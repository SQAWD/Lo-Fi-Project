using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager2 : MonoBehaviour
{
    
    [Header("Limit Per List")]
    private int maxThemes = 1;
    private int maxItems = 3;
    private int maxSounds = 1;

    [Header("Selected Theme Properties")]
    public Material SelectedThemelayerOne;
    public Material SelectedThemelayerTwo;
    public Material SelectedThemelayerThree;
    public Material SelectedThemelayerFour; 
    public Material SelectedThemelayerFive;

    [Header("Selected Item Properties")]
    public Transform ItemZone1;
    public Transform ItemZone2;
    public Transform ItemZone3;

    [Header("Selected Sound Properties")]
    public AudioSource SelectedSound;
    public Image SelectedSoundFilter;
    

    [Header("Shopping Cart Preview Lists")]
    public List<SOthemes> ShopCartPreviewThemesList = new List<SOthemes>();
    public List<SOitems> ShopCartPreviewItemsList = new List<SOitems>();
    public List<SOsounds> ShopCartPreviewSoundsList = new List<SOsounds>();

    [Header("Selected Lists")]
    public List<SOthemes> SelectedThemesList = new List<SOthemes>();
    public List<SOitems> SelectedItemsList = new List<SOitems>();
    public List<SOsounds> SelectedSoundsList = new List<SOsounds>();

    // Start is called before the first frame update
    void Start()
    {
         if (SelectedSoundsList.Count > 0)
        {
            SelectedSound.clip = SelectedSoundsList[0].SoundFile;
            SelectedSound.Play();
        }
    }


    public void ThemeToPreviewList(SOthemes theme)
    {
        //Click Add Item to Cart, Second Click Removes From Cart
        if (ShopCartPreviewThemesList.Contains(theme))
        {
            ShopCartPreviewThemesList.Remove(theme);
        }
        else
        {
            ShopCartPreviewThemesList.Add(theme);
        }
    }

    public void AddThemeToSelectedList(SOthemes theme)
    {
        if (SelectedThemelayerOne != null)
        {
            SelectedThemelayerOne.mainTexture= theme.ThemelayerOne;
            SelectedThemelayerOne.SetTexture("_EmissionMap", theme.ThemelayerOne);
            SelectedThemelayerOne.EnableKeyword("_EMISSION");
        }

    }

    public void AddSoundToSelectedList(SOsounds sound)
    {
         if (SelectedSoundsList.Count == 1)
    {
        // Replace the current item with the new sound
        SelectedSoundsList[0] = sound;
    }
    // Check if the list is empty
    else if (SelectedSoundsList.Count == 0)
    {
        // Add the new sound to the list
        SelectedSoundsList.Add(sound);
    }

    // Update the AudioSource with the new sound
     if (SelectedSoundsList.Count > 0)
     {
            SelectedSound.clip = SelectedSoundsList[0].SoundFile;
            SelectedSound.Play();
        }
    }

}

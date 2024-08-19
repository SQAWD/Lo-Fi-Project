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

    public GameObject SelectedThemeLayerOneGameObject;
    public GameObject SelectedThemeLayerTwoGameObject;
    public GameObject SelectedThemeLayerThreeGameObject;
    public GameObject SelectedThemeLayerFourGameObject;
    public GameObject SelectedThemeLayerFiveGameObject;

    [SerializeField] float LayerOneScrollSpeed;

    private Layer1TextureOffset LayerOneTextureOffSetScript;

    [Header("Selected Sound Properties")]
    public AudioSource SelectedSound;
    public int SoundPreviewTime;
    public Image SelectedSoundFilter;
    

    [Header("Shopping Cart Preview Lists")]
    public List<SOthemes> ShopCartPreviewThemesList = new List<SOthemes>();
    public List<SOitems> ShopCartPreviewItemsList = new List<SOitems>();
    public List<SOsounds> ShopCartPreviewSoundsList = new List<SOsounds>();

    [Header("Selected Lists")]
    public List<SOthemes> SelectedThemesList = new List<SOthemes>();
    public List<SOitems> SelectedItemsList = new List<SOitems>();
    public List<SOsounds> SelectedSoundsList = new List<SOsounds>();

    private Coroutine previewCoroutine;

    // Start is called before the first frame update
    void Start()
    {
         if (SelectedSoundsList.Count > 0)
        {
            SelectedSoundsList[0].selected = true;
            SelectedSound.clip = SelectedSoundsList[0].SoundFile;
            SelectedSound.Play();
        }

         if (SelectedThemesList.Count > 0)
        {
            SelectedThemesList[0].selected = true;
        }


    }


    public void AddThemeToPreviewList(SOthemes theme)
    {
        //Debug.LogError("Theme Added to Preview List");

        if (ShopCartPreviewThemesList.Count == 1)
    {
        // Set SOTheme previewON bool to False
        ShopCartPreviewThemesList[0].previewOn = false;
        
        // Replace the current item with the new sound
        ShopCartPreviewThemesList[0] = theme;
    }
    // Check if the list is empty
    else if (ShopCartPreviewThemesList.Count == 0)
    {
        // Add the new sound to the list
        ShopCartPreviewThemesList.Add(theme);
    }


    }

    public void AddThemeToSelectedList(SOthemes theme)
    {
            if (SelectedThemesList.Count == 1)
        {
            // Set SOTheme previewON bool to False
            SelectedThemesList[0].previewOn = false;
        
            // Replace the current item with the new sound
            SelectedThemesList[0] = theme;
        }
            // Check if the list is empty
            else if (SelectedThemesList.Count == 0)
        {
             // Add the new sound to the list
             SelectedThemesList.Add(theme);
        }

         UpdateAllLayers(theme);
    }

    public struct LayerConfig
{
    public bool IsEnabled;
    public GameObject LayerGameObject;
    public Material LayerMaterial;
    public Texture LayerTexture;
}

    private void UpdateAllLayers(SOthemes theme)
{
    LayerConfig[] layers = new LayerConfig[]
    {
        new LayerConfig { IsEnabled = theme.LayerOneEnabled, LayerGameObject = SelectedThemeLayerOneGameObject, LayerMaterial = SelectedThemelayerOne, LayerTexture = theme.ThemelayerOne },
        new LayerConfig { IsEnabled = theme.LayerTwoEnabled, LayerGameObject = SelectedThemeLayerTwoGameObject, LayerMaterial = SelectedThemelayerTwo, LayerTexture = theme.ThemelayerTwo },
        new LayerConfig { IsEnabled = theme.LayerThreeEnabled, LayerGameObject = SelectedThemeLayerThreeGameObject, LayerMaterial = SelectedThemelayerThree, LayerTexture = theme.ThemelayerThree },
        new LayerConfig { IsEnabled = theme.LayerFourEnabled, LayerGameObject = SelectedThemeLayerFourGameObject, LayerMaterial = SelectedThemelayerFour, LayerTexture = theme.ThemelayerFour },
        new LayerConfig { IsEnabled = theme.LayerFiveEnabled, LayerGameObject = SelectedThemeLayerFiveGameObject, LayerMaterial = SelectedThemelayerFive, LayerTexture = theme.ThemelayerFive }
    };

    foreach (var layer in layers)
    {
        UpdateLayer(layer);
    }
}

private void UpdateLayer(LayerConfig layerConfig)
{
    if (layerConfig.IsEnabled)
    {
        layerConfig.LayerGameObject.SetActive(true);
        layerConfig.LayerMaterial.mainTexture = layerConfig.LayerTexture;
        layerConfig.LayerMaterial.SetTexture("_EmissionMap", layerConfig.LayerTexture);
        layerConfig.LayerMaterial.EnableKeyword("_EMISSION");
    }
    else
    {
        layerConfig.LayerGameObject.SetActive(false);
    }
}


    public void AddSoundToSelectedList(SOsounds sound)
    {
         if (SelectedSoundsList.Count == 1)
    {
        // Set Selection to False
        SelectedSoundsList[0].selected = false;

        // Replace the current item with the new sound
        SelectedSoundsList[0] = sound;

    }
    // Check if the list is empty
    else if (SelectedSoundsList.Count == 0)
    {
        // Add the new sound to the list
        SelectedSoundsList.Add(sound);
    }

     // Set the selected property of the new sound to true
    sound.selected = true;

    // Update the AudioSource with the new sound
     if (SelectedSoundsList.Count > 0)
     {
            SelectedSound.clip = SelectedSoundsList[0].SoundFile;
            SelectedSound.Play();
        }
    }

    public void AddSoundToShopCartPreviewList(SOsounds sound)
    {
           if (ShopCartPreviewSoundsList.Count == 1)
    {
        
        // Set SOsound previewON bool to False
        ShopCartPreviewSoundsList[0].previewOn = false;
        
        // Replace the current item with the new sound
        ShopCartPreviewSoundsList[0] = sound;
    }
    // Check if the list is empty
    else if (ShopCartPreviewSoundsList.Count == 0)
    {
        // Add the new sound to the list
        ShopCartPreviewSoundsList.Add(sound);
    }

    // Stop any currently running preview coroutine
        if (previewCoroutine != null)
        {
            StopCoroutine(previewCoroutine);
        }

        // Start the preview coroutine
        previewCoroutine = StartCoroutine(PlayPreviewSoundCoroutine(sound));
    }

  // Coroutine to play the preview sound for 15 seconds
    private IEnumerator PlayPreviewSoundCoroutine(SOsounds sound)
    {
        // turn on preview animation
        sound.previewOn = true;
        
        // Stop the selected sound
        if (SelectedSound.isPlaying)
        {
            SelectedSound.Stop();
        }

        // Play the preview sound
        SelectedSound.clip = sound.SoundFile;
        SelectedSound.Play();

        // Wait for 15 seconds
        yield return new WaitForSeconds(SoundPreviewTime);

        // Stop the preview sound
        SelectedSound.Stop();

        // Turn off Preview Animation
        sound.previewOn = false;

        // Resume playing the selected sound if available
        if (SelectedSoundsList.Count > 0)
        {
            SelectedSound.clip = SelectedSoundsList[0].SoundFile;
            SelectedSound.Play();
        }

        // Clear the coroutine reference
        previewCoroutine = null;
    }


}

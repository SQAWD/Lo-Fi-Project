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
    public Material ThemeLayerOneMaterial;
    public Material SelectedThemelayerTwo;
    public Material SelectedThemelayerThree;
    public Material SelectedThemelayerFour; 
    public Material SelectedThemelayerFive;

    public GameObject SelectedThemeLayerOneGameObject;
    public GameObject SelectedThemeLayerTwoGameObject;
    public GameObject SelectedThemeLayerThreeGameObject;
    public GameObject SelectedThemeLayerFourGameObject;
    public GameObject SelectedThemeLayerFiveGameObject;

    private Layer1TextureOffset LayerOneTextureOffSetScript;

    [Header("Selected Sound Properties")]
    public AudioSource SelectedSound;
    public GameObject WeatherGameObject;
    public Image SelectedSoundFilter;
    public Material WeatherMaterial;
    public int SoundPreviewTime;
    public int SpriteFrameRate = 25;
    private Material weatherMaterial;

    private Coroutine spriteSequenceCoroutine;
    

    [Header("Shopping Cart Preview Lists")]
    public List<SOthemes> ShopCartPreviewThemesList = new List<SOthemes>();
    public List<SOitems> ShopCartPreviewItemsList = new List<SOitems>();
    public List<SOsounds> ShopCartPreviewSoundsList = new List<SOsounds>();

    [Header("Selected Lists")]
    public List<SOthemes> SelectedThemesList = new List<SOthemes>();
    public List<SOitems> SelectedItemsList = new List<SOitems>();
    public List<SOsounds> SelectedSoundsList = new List<SOsounds>();

    private Coroutine previewCoroutine;
    private Coroutine themePreviewCoroutine;
    private Coroutine itemPreviewCoroutine;

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

#region  Themes

    public void AddThemeToShopCartPreviewList(SOthemes theme)
    {
        if (ShopCartPreviewThemesList.Count == 1)   
        {
            // Set SOTheme previewON bool to False
            ShopCartPreviewThemesList[0].previewOn = false;
            
            // Replace the current item with the new theme
            ShopCartPreviewThemesList[0] = theme;
        }
        // Check if the list is empty
        else if (ShopCartPreviewThemesList.Count == 0)
        {
            // Add the new theme to the list
            ShopCartPreviewThemesList.Add(theme);
        }

        // Stop any currently running preview coroutine
        if (themePreviewCoroutine != null)
        {
            StopCoroutine(themePreviewCoroutine);
        }


        // Start the preview coroutine
        themePreviewCoroutine = StartCoroutine(PreviewThemeCoroutine(theme));


    }

    private IEnumerator PreviewThemeCoroutine(SOthemes theme)
    {
        // Apply the preview theme
        UpdateAllLayers(theme);

        // Set preview flag
        theme.previewOn = true;

        // Wait for some condition or until the theme is removed from the preview list
        while (ShopCartPreviewThemesList.Contains(theme))
        {
            yield return null;
        }

        // Revert back to the selected theme
        if (SelectedThemesList.Count > 0)
        {
            UpdateAllLayers(SelectedThemesList[0]);
        }

        // Clear the coroutine reference
        themePreviewCoroutine = null;
    }

     public void RemoveThemeFromShopCartPreviewList(SOthemes theme)
    {
        if (ShopCartPreviewThemesList.Contains(theme))
        {
            ShopCartPreviewThemesList.Remove(theme);
            theme.previewOn = false;

            // If the removed theme was being previewed, stop the coroutine
            if (themePreviewCoroutine != null)
            {
                StopCoroutine(themePreviewCoroutine);
                themePreviewCoroutine = null;
            }

            // Revert back to the selected theme
            if (SelectedThemesList.Count > 0)
            {
                UpdateAllLayers(SelectedThemesList[0]);
            }
        }
    }


    public void AddThemeToSelectedList(SOthemes theme)
    {
            // if the list already has an item
            if (SelectedThemesList.Count == 1)
        {
            // Set removed SOTheme previewON bool to False
            SelectedThemesList[0].previewOn = false;
        
            // Replace the current theme with the new theme
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
        new LayerConfig { IsEnabled = theme.LayerOneEnabled, LayerGameObject = SelectedThemeLayerOneGameObject, LayerMaterial = ThemeLayerOneMaterial, LayerTexture = theme.ThemelayerOne },
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
        
        // Use sharedMaterial to avoid creating instances
        Material sharedMat = layerConfig.LayerGameObject.GetComponent<Renderer>().sharedMaterial;

        // Update the shared material's textures
        sharedMat.mainTexture = layerConfig.LayerTexture;
        sharedMat.SetTexture("_EmissionMap", layerConfig.LayerTexture);
        sharedMat.EnableKeyword("_EMISSION");
    }
    else
    {
        layerConfig.LayerGameObject.SetActive(false);
    }

}

#endregion 

#region  Sounds

public void AddSoundToSelectedList(SOsounds sound)

    {
     if (SelectedSoundsList.Count == 1)
    {
        // Set Selection to False
        SelectedSoundsList[0].selected = false;

        // Replace the current item with the new sound
        SelectedSoundsList[0] = sound;
    }
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

    // Ensure the Weather GameObject is active if there is a sprite filter
    if (sound.SoundFilterSprite != null && sound.SoundFilterSprite.Count > 0)
    {
        WeatherGameObject.SetActive(true);
    }
    else
    {
        WeatherGameObject.SetActive(false);
    }

    // Stop any existing sprite sequence before starting a new one
    if (spriteSequenceCoroutine != null)
    {
        StopCoroutine(spriteSequenceCoroutine);
    }

    // Check the FilterAnimation bool in the sound scriptable object
    if (sound.FilterAnimation)
    {
        // Play the sprite sequence associated with the sound
        PlaySpriteSequenceOnMaterial(sound.SoundFilterSprite);
    }
    else
    {
        // Deactivate the Weather GameObject if animation is not enabled
        if (WeatherGameObject != null)
        {
            WeatherGameObject.SetActive(false);
        }
    }

        
    }

    public void PlaySpriteSequenceOnMaterial(List<Sprite> sprites)
    {
        if (WeatherGameObject != null)
        {
            WeatherGameObject.SetActive(true);  // Ensure Weather is active when playing the sequence
        }

        if (spriteSequenceCoroutine != null)
        {
            StopCoroutine(spriteSequenceCoroutine);
        }

        spriteSequenceCoroutine = StartCoroutine(SpriteSequenceCoroutine(sprites));
    }

    private IEnumerator SpriteSequenceCoroutine(List<Sprite> sprites)
    {
        float frameDuration = 1.0f / SpriteFrameRate;  // Calculate frame duration based on frame rate

        if (WeatherMaterial != null && sprites != null)
        {
        while (true) // Infinite loop to keep cycling through the sprites
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite != null)
                {
                    WeatherMaterial.mainTexture = sprite.texture;
                    yield return new WaitForSeconds(frameDuration);  // Use the calculated frame duration
                }
            }
        }
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
#endregion

#region Items

public void AddItemToShopCartPreviewList(SOitems item)
    {
        if (ShopCartPreviewItemsList.Count == 1)   
        {
            // Set SOTheme previewON bool to False
            ShopCartPreviewItemsList[0].previewOn = false;
            
            // Replace the current item with the new theme
            ShopCartPreviewItemsList[0] = item;
        }
        // Check if the list is empty
        else if (ShopCartPreviewItemsList.Count == 0)
        {
            // Add the new theme to the list
            ShopCartPreviewItemsList.Add(item);
        }

        // Stop any currently running preview coroutine
        if (itemPreviewCoroutine != null)
        {
            StopCoroutine(itemPreviewCoroutine);
        }

    }

    public void AddItemToSelectedList(SOitems items)

    {
     if (SelectedSoundsList.Count == 1)
    {
        // Set Selection to False
        SelectedItemsList[0].selected = false;

        // Replace the current item with the new sound
        SelectedItemsList[0] = items;
    }
    else if (SelectedItemsList.Count == 0)
    {
        // Add the new sound to the list
        SelectedItemsList.Add(items);
    }

    // Set the selected property of the new sound to true
    items.selected = true;

        
    }









#endregion
}

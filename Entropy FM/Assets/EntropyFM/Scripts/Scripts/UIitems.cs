using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIitems : MonoBehaviour
{
    [HideInInspector] public SOitems item;
    public TMP_Text nameTxt;
    public TMP_Text costTxt;
    public Image thumbnailImg;
    public Image backgroundImg;
    public Button removeitemButton;
    public bool purchasedBool;
    public bool selectedBool;
    public bool previewBool;
    public bool removeitemBool;
    public bool iteminSceneUIEnabled;
    private bool animationUIEnabled;

    [Header("Game Objects")]
    public GameObject costGameObject; 
    public GameObject nameGameObject; 
    public GameObject selectedOutlineGameObject;
    public GameObject ThumbnailGameObject;
    public GameObject spriteAnimationGameObject;  // GameObject that will display the sprite sequence
    public GameObject removeItemGameObject;

    private Image spriteAnimationImage;  // Cache the Image component for the sprite animation
    private Coroutine spriteSequenceCoroutine;
    public float SpriteFrameRate = 30f;  // Frames per second for the sprite animation

    void Start()
    {
        nameTxt.text = item.name;
        costTxt.text = item.cost.ToString();
        thumbnailImg.sprite = item.thumbnail;

        // Cache the Image component from the spriteAnimationGameObject
        if (spriteAnimationGameObject != null)
        {
            spriteAnimationImage = spriteAnimationGameObject.GetComponent<Image>();
        }

        // Check if the animation should start immediately
        if (item.AnimationEnabled)
        {
            animationUIEnabled = item.AnimationEnabled;
            UpdateAnimationUI();  // Ensure the animation is started at the beginning
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item.selected != selectedBool)
        {
            selectedBool = item.selected;
            UpdateSelectedUI();
        }

        if (item.purchased != purchasedBool)
        {
            purchasedBool = item.purchased;
            UpdateCostUI();
        }

        if (item.previewOn != previewBool)
        {
            previewBool = item.previewOn;
            UpdatePreviewUI();
        }

        if (item.ShowRemoveUIBool != removeitemBool)
        {
            removeitemBool = item.ShowRemoveUIBool;
            UpdateRemoveUI();
        }

        if (item.AnimationEnabled != animationUIEnabled)
        {
            animationUIEnabled = item.AnimationEnabled;
            UpdateAnimationUI();
        }

        if (iteminSceneUIEnabled == true){
            UpdateInSceneItemUI();
        }

    }

    void UpdateSelectedUI()
    {
        selectedOutlineGameObject.SetActive(selectedBool);
    }

    void UpdateInSceneItemUI()
    {
        selectedOutlineGameObject.SetActive(false);
        costGameObject.SetActive(false);
        nameGameObject.SetActive(false);
        backgroundImg.enabled = false;
        
    }

    void UpdateCostUI()
    {
        costGameObject.SetActive(!purchasedBool);
    }

    void UpdatePreviewUI()
    {
        spriteAnimationGameObject.SetActive(previewBool);
    }

    void UpdateRemoveUI()
    {
        removeItemGameObject.SetActive(removeitemBool);
    }

    void UpdateAnimationUI()
    {
        // If animation is enabled, deactivate the thumbnail and activate the sprite animation gameobject
        ThumbnailGameObject.SetActive(!animationUIEnabled);
        spriteAnimationGameObject.SetActive(animationUIEnabled);

        if (animationUIEnabled && spriteAnimationImage != null)
        {
            // Play the sprite sequence from item.SpriteImageSequence at 30 fps
            if (spriteSequenceCoroutine != null)
            {
                StopCoroutine(spriteSequenceCoroutine);
            }

            if (item.SpriteImageSequence != null && item.SpriteImageSequence.Count > 0)
            {
                spriteSequenceCoroutine = StartCoroutine(PlaySpriteSequence(item.SpriteImageSequence));
            }
        }
        else if (!animationUIEnabled && spriteSequenceCoroutine != null)
        {
            // Stop the sprite sequence if the animation is disabled
            StopCoroutine(spriteSequenceCoroutine);
            spriteSequenceCoroutine = null;
        }
    }

    // Method to play the sprite sequence on the Image component at 30 fps
    public IEnumerator PlaySpriteSequence(List<Sprite> sprites)
    {
        float frameDuration = 1.0f / SpriteFrameRate;  // Calculate frame duration based on frame rate

        while (true)  // Infinite loop to keep cycling through the sprites
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite != null && spriteAnimationImage != null)
                {
                    spriteAnimationImage.sprite = sprite;  // Assign the sprite to the Image component
                    yield return new WaitForSeconds(frameDuration);  // Wait for the frame duration
                }
            }
        }
    }
}

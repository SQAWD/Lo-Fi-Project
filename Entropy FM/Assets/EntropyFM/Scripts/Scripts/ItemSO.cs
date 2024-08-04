using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "ItemMenu", menuName = "old Item")]
public class ItemSO : ScriptableObject
{
    [Header("General")]
    public new string name;
    public bool isPurchased; 

    public ItemType itemType;

    [Header("Theme Settings")]
    public ThemeType themeType;
    public string itemID;
    public int cost; 
    public Sprite thumbnail;

    public GameObject model;

    [Header("Moving Textures")]
    public Texture ThemelayerOne;
    public Texture ThemelayerTwo;
    public Texture ThemelayerThree;
    public Texture ThemelayerFour; 
    public Texture ThemelayerFive;
    public Texture ThemeWall;


    [Header("Sounds Settings")]
    public Color SoundFilterOverlayColor = Color.white; // Default to white color
    public AudioClip Sound;
    public int SoundFilterSpeed = 25; // Default value set to 25
    public Sprite[] SoundFilterSprite;
    

    public static event Action<ItemSO> OnSoundItemAdded;

    // Call this method when the item is added to the 'Selected Sounds' list
    public void AddToSelectedSounds()
    {
        if (itemType == ItemType.Sound)
        {
            OnSoundItemAdded?.Invoke(this);
        }
    }
}

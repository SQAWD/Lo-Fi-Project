using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOsound", menuName = "New SOsound")]
public class SOsounds : ScriptableObject
{
    [Header("General")]
    public new string name;
    public int cost; 
    public Sprite thumbnail;
    public string itemID;

    [Header("Info")]
    public Color SoundFilterOverlayColor = Color.white; // Default to white color
    public AudioClip Sound;
    public int SoundFilterSpeed = 25; // Default value set to 25
    public Sprite[] SoundFilterSprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

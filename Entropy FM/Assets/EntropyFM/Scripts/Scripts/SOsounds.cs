using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundMenu", menuName = "New Sound")]
public class SOsounds : ScriptableObject
{
    [Header("General")]
    public new string name;
    public int cost; 
    public Sprite thumbnail;
    public string itemID;
    public bool purchased;
    public bool selected;
    public bool previewOn;

    [Header("Properties")]
    public Color SoundFilterOverlayColor = Color.white; // Default to white color
    public AudioClip SoundFile;
    public bool EnableAnimation;
    public List<Sprite> SoundFilterSprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

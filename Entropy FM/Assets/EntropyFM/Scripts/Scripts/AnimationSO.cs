using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationMenu", menuName = "New Animation")]
public class AnimationSO : ScriptableObject
{
    [Header("General")]
    public new string name;
    public int cost; 
    public Sprite thumbnail;
    public string itemID;
    public bool purchased;
    public bool selected;
    public bool previewOn;


    public Color SoundFilterOverlayColor = Color.white;
    public bool AnimationEnabled;
    public List<Sprite> SpriteImageSequence;
    public Transform AnimationLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

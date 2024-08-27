using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Menu", menuName = "New Item")]
public class SOitems : ScriptableObject
{
     [Header("General")]
    public new string name;
    public int cost; 
    public Sprite thumbnail;
    public string itemID;
    public bool purchased;
    public bool selected;
    public bool previewOn;

    [Header("Item Properties")]
    public bool AnimationEnabled;
    public Color SoundFilterOverlayColor = Color.white;
    public Vector3 AnimationLocation;
    public List<Sprite> SpriteImageSequence;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

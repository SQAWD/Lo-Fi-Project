using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThemeMenu", menuName = "New Theme")]
public class SOthemes : ScriptableObject
{
    [Header("General")]
    public new string name;
    public int cost; 
    public Sprite thumbnail;
    public string itemID;
    public bool purchased;
    public bool selected;
    public bool previewOn;

    [Header("Theme Properties")]
    public Texture ThemelayerOne;
    public float LayerOneScrollSpeed;
    public Texture ThemelayerTwo;
    public float LayerTwoScrollSpeed;
    public Texture ThemelayerThree;
    public float LayerThreeScrollSpeed;
    public bool LayerThreeEnabled;
    public Texture ThemelayerFour; 
    public float LayerFourScrollSpeed;
    public bool LayerFourEnabled;
    public Texture ThemelayerFive;
    public float LayerFiveScrollSpeed;
    public bool LayerFiveEnabled;
    public Texture ThemeWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

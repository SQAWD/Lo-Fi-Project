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

    [Header("Layer One")]
    public bool LayerOneEnabled;
    public Texture ThemelayerOne;
    public float LayerOneScrollSpeed;
    

    [Header("Layer Two")]
    public bool LayerTwoEnabled;
    public Texture ThemelayerTwo;
    public float LayerTwoScrollSpeed;

    [Header("Layer Three")]
    public bool LayerThreeEnabled;
    public Texture ThemelayerThree;
    public float LayerThreeScrollSpeed;
    

    [Header("Layer Four")]
    public bool LayerFourEnabled;
    public Texture ThemelayerFour; 
    public float LayerFourScrollSpeed;
    

    [Header("Layer Five")]
    public bool LayerFiveEnabled;
    public Texture ThemelayerFive;
    public float LayerFiveScrollSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

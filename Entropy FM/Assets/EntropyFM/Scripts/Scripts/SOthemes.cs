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

    [Header("Theme Properties")]
    public Texture ThemelayerOne;
    public Texture ThemelayerTwo;
    public Texture ThemelayerThree;
    public Texture ThemelayerFour; 
    public Texture ThemelayerFive;
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

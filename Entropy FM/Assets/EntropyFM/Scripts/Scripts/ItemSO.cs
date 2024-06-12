using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemMenu", menuName ="New Item")]
public class ItemSO : ScriptableObject
{
    public new String name;
    public String itemID;
    public int cost; 
    public Sprite thumbnail;

    public ItemType itemType;

    public ThemeType themeType;

    public bool isPurchased; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

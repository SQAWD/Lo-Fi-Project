using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class UIthemes : MonoBehaviour
{
    public SOthemes theme;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt; 
    public GameObject costObject;
    public Image thumbnailImg;
    public bool purchasedBool;

    // Start is called before the first frame update
    void Start()
    {
        nameTxt.text = theme.name;
        thumbnailImg.sprite = theme.thumbnail;
        costTxt.text = theme.cost.ToString();
        purchasedBool = theme.purchased;

        if (purchasedBool == false)
        {
          costObject.gameObject.SetActive(true);
        }
        else
        {
            //No need to show cost if item is purchased 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

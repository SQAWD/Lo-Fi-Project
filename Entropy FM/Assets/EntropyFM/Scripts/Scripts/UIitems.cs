using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIitems : MonoBehaviour
{

    public SOitems item;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt; 
    public Image thumbnailImg;
    // Start is called before the first frame update
    void Start()
    {
        nameTxt.text = item.name;
        costTxt.text = item.cost.ToString();
        thumbnailImg.sprite = item.thumbnail;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

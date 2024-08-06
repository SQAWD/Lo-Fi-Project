using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIthemes : MonoBehaviour
{
    public SOthemes theme;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt; 
    public Image thumbnailImg;

    // Start is called before the first frame update
    void Start()
    {
        nameTxt.text = theme.name;
        costTxt.text = theme.cost.ToString();
        thumbnailImg.sprite = theme.thumbnail;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

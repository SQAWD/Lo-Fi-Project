using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIsounds : MonoBehaviour
{
    public SOsounds sound;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt; 
    public Image thumbnailImg;

    // Start is called before the first frame update
    void Start()
    {
        thumbnailImg.sprite = sound.thumbnail;
        nameTxt.text = sound.name; 
        costTxt.text = sound.cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

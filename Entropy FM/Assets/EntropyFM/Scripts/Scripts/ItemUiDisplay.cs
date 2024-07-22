using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUiDisplay : MonoBehaviour
{
    public ItemSO item;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt; 
    public Image thumbnailImg;
    public Transform modelPlaceholder; // Placeholder for the model

    void Start()
    {
        nameTxt.text = item.name;
        costTxt.text = item.cost.ToString();
        thumbnailImg.sprite = item.thumbnail;

        // Instantiate the model and place it in the modelPlaceholder
        if (item.model != null)
        {
            Instantiate(item.model, modelPlaceholder);
        }
    }

    void Update()
    {
        
    }
}

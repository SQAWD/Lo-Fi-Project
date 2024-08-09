using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIsounds : MonoBehaviour
{
   [Header("Ui Elements")]
    public SOsounds sound;
    public TMP_Text nameTxt; 
    public TMP_Text costTxt;
    public Image thumbnailImg;
    public bool purchasedBool;

    [Header("Bools")]
    public bool selectedBool;
    public bool previewBool;

    [Header("Game Objects")]
    public GameObject costObject; 
    public GameObject selectedObject;
    public GameObject previewUI;

    // Start is called before the first frame update
    void Start()
    {
        thumbnailImg.sprite = sound.thumbnail;
        nameTxt.text = sound.name; 
        costTxt.text = sound.cost.ToString();
        purchasedBool = sound.purchased;
        selectedBool = sound.selected;
        previewBool = sound.previewOn;

        UpdateSelectedUI();
        UpdateCostUI();

    }

     // Update is called once per frame
    void Update()
    {
        if (sound.selected != selectedBool)
        {
            selectedBool = sound.selected;
            UpdateSelectedUI();
        }

        if (sound.purchased != purchasedBool)
        {
          purchasedBool = sound.purchased;
          UpdateCostUI();
        }

        if (sound.previewOn != previewBool)
        {
          previewBool = sound.previewOn;
          UpdatePreviewUI();
        }

    }

    void UpdateSelectedUI()
    {
        selectedObject.SetActive(selectedBool);
    }

      void UpdateCostUI()
    {
        costObject.SetActive(!purchasedBool);
    }

      void UpdatePreviewUI()
    {
        previewUI.SetActive(previewBool);
    }
}

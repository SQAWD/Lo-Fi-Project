using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("General")]
    public SOitems item;
    public Transform specificParent;  // Variable to hold the specific parent


    [Header("UI")]
    public Image image;
    public Image outlineimage;
    public UIitems UIitems;
    public Text countText;
    public InventorySlot inventorySlot;

    [Header("Property")]
    public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitialiseItem(SOitems newItem) {
        item = newItem;
        UIitems.item = newItem;
        UIitems.nameTxt.text = newItem.name;
        UIitems.costTxt.text = newItem.cost.ToString();
        UIitems.thumbnailImg.sprite = newItem.thumbnail;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        
        if (specificParent != null) {
            transform.SetParent(specificParent);
            transform.SetAsLastSibling();  // Move to the top of the layer order under specificParent
            image.raycastTarget = false;
            outlineimage.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        outlineimage.raycastTarget = true;
        inventorySlot.SlotUIEnabled = false;
    }
}

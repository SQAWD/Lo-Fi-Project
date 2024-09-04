using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform specificParent;  // Variable to hold the specific parent
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public Image outlineimage;

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
    }
}

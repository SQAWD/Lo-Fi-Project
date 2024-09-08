using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public bool SlotUIEnabled;
    public GameObject OutlineUI1;
    public GameObject OutlineUI2;
    public TestingUI testingUI;

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0){
        GameObject dropped = eventData.pointerDrag;
        InventoryItem InventoryItem = dropped.GetComponent<InventoryItem>();
        InventoryItem.parentAfterDrag = transform;
        }

        // Force layout update after item is dropped in the slot
            LayoutGroup layoutGroup = GetComponentInParent<LayoutGroup>();
            if (layoutGroup != null)
            {
                layoutGroup.enabled = false;  // Temporarily disable to force update
                layoutGroup.enabled = true;   // Re-enable to reapply the layout
            }
}

void Start(){
    UpdateSlotUI();
}

void Update(){
    if (testingUI.ItemTargetsEnabled != SlotUIEnabled)
        {
            SlotUIEnabled = testingUI.ItemTargetsEnabled;
            UpdateSlotUI();
        }

}

public void UpdateSlotUI(){
    OutlineUI1.gameObject.SetActive(SlotUIEnabled);
    OutlineUI2.gameObject.SetActive(SlotUIEnabled);
    Debug.LogError("TurnoffSlotsUI");
}

}

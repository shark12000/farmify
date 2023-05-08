using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    private Transform _startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(transform.root);
        ShopManager.instance.OnBeginDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        transform.SetParent(_startParent);

        Slot slot = FindInventorySlotUnderMouse();
        if (slot != null)
        {
            if (this.GetComponent<ShopItem>() != null)
            {
                ShopManager.instance.OnEndDragShopItem();
            }
            else if (this.GetComponent<InventoryItem>() != null)
            {
                if (slot.IsEmpty())
                {
                    Debug.Log("Before: " +slot.IsEmpty());
                    transform.SetParent(slot.transform, false);
                    Debug.Log("After: " +slot.IsEmpty());
                }
                else
                {
                    InventoryItem draggedInventoryItem = GetComponent<InventoryItem>();
                    Debug.Log("draggedInventoryItem: " + draggedInventoryItem);
                    InventoryItem inventoryItemFromSlot = slot.GetComponentInChildren<InventoryItem>();
                    Debug.Log("inventoryItemFromSlot: " + inventoryItemFromSlot);
                    if (inventoryItemFromSlot.item.title == draggedInventoryItem.item.title)
                    {
                        inventoryItemFromSlot.count++;
                        Debug.Log(inventoryItemFromSlot.count);
                        inventoryItemFromSlot.RefreshCount();
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else
        {
            ShopManager.instance.OnEndDragShopItem();
        }
    }
    
    private Slot FindInventorySlotUnderMouse()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            Slot slot = result.gameObject.GetComponent<Slot>();
            if (slot != null && result.gameObject.CompareTag(@"Inventory"))
            {
                Debug.Log("SLOTTT" + slot);
                return slot;
            }
        }

        return null;
    }
}

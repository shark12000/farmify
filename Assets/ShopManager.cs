using System.Collections.Generic;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour {
    [SerializeField] private List<Item> items;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Slot shopSlotPrefab;
    [SerializeField] private ShopItem shopItemPrefab;
    [SerializeField] private InventoryItem inventoryItemPrefab;

    private Item draggedShopItem;
    private InventoryItem draggedInventoryItem;
    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BuyItem(Item item, Slot slot)
    {
        if (MoneyManager.instance.GetCoins() >= item.price)
        {
            InventoryManager.instance.AddItemToSpecificSlot(slot, item);
            MoneyManager.instance.SpendCoins(item.price);
        }
        else
        {
            Debug.Log("Not enough money to buy this item!");
        }
    }

    public void SellItem(InventoryItem item)
    {
        InventoryManager.instance.RemoveItem(item);
        MoneyManager.instance.AddCoins(item.item.price*item.count);
    }

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (Item item in items)
        {
            Slot newSlot = Instantiate(shopSlotPrefab, shop.transform);
            ShopItem shopItem = Instantiate(shopItemPrefab, newSlot.transform);
            shopItem.InitialiseItem(item);
        }
    }

    public void OnBeginDrag(Draggable draggable)
    {

        ShopItem shopItem = draggable.GetComponentInParent<ShopItem>();
        if (shopItem != null)
        {
            draggedShopItem = shopItem.item;
            return;
        }

        InventoryItem inventoryItem = draggable.GetComponentInParent<InventoryItem>();
        if (inventoryItem != null)
        {
            draggedInventoryItem = inventoryItem;
            return;
        }
    }
    
    public void OnEndDragShopItem()
    {
        if (draggedShopItem != null)
        {
            Slot targetSlot = FindInventorySlotUnderMouse();
            
            if (targetSlot != null && targetSlot.IsEmpty())
            {
                InventoryManager.instance.SpawnNewItem(draggedShopItem, targetSlot);
                BuyItem(draggedShopItem, targetSlot);
            }
            else if (!targetSlot.IsEmpty())
            {
                InventoryItem itemInSlot = targetSlot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item.title == draggedShopItem.title)
                {
                    BuyItem(draggedShopItem, targetSlot);
                }
            }
        }
        draggedShopItem = null;

        if (draggedInventoryItem != null)
        {
            Slot targetSlot = FindInventorySlotUnderMouse();

            if (targetSlot != null && (targetSlot.IsEmpty() || draggedInventoryItem.item.stackable))
            {
                if (targetSlot.IsEmpty())
                {
                    InventoryManager.instance.SpawnNewInventoryItem(draggedInventoryItem, targetSlot);
                }
                else
                {
                    Debug.Log("count" + draggedInventoryItem.count);
                    draggedInventoryItem.count++;
                }

                InventoryManager.instance.RemoveItemAfterDragging(draggedInventoryItem);

                if (IsShopObjectUnderMouse())
                {
                    SellItem(draggedInventoryItem);
                }
            }
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
            
            Debug.Log(result);
            Slot slot = result.gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }


    private bool IsShopObjectUnderMouse()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log(result);
            GameObject shopObject = result.gameObject.transform.parent.gameObject;
            if (shopObject != null && shopObject.CompareTag("Shop"))
            {
                return true;
            }
        }
        return false;
    }
    
}
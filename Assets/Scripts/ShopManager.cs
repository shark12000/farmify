using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour {
    [SerializeField] private List<Item> items;
    [SerializeField] private GameObject shop;
    [SerializeField] private Slot shopSlotPrefab;
    [SerializeField] private ShopItem shopItemPrefab;

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

    public void BuyItem(Item item, Slot slot, bool isExist)
    {
        if (MoneyManager.instance.GetCoins() >= item.price)
        {
            if (!isExist)
            {
                InventoryManager.instance.SpawnNewItem(draggedShopItem, slot);
                InventoryManager.instance.AddItemToSpecificSlot(slot, item);
                MoneyManager.instance.SpendCoins(item.price);
            }
            else
            {
                InventoryManager.instance.AddItemToSpecificSlot(slot, item);
                MoneyManager.instance.SpendCoins(item.price);
            }
        }
        else
        {
            Debug.Log("Not enough money to buy this item!");
        }
    }

    public void SellItem(InventoryItem item)
    {
        InventoryManager.instance.RemoveItem(item);
        Debug.Log("Amount to add" + item.item.price*item.count);
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
        HandleShopItem();
        HandleInventoryItem();
    }

    private void HandleShopItem()
    {
        if (draggedShopItem != null)
        {
            Slot targetSlot = FindInventorySlotUnderMouse();

            if (targetSlot != null && targetSlot.IsEmpty())
            {
                BuyItem(draggedShopItem, targetSlot, false);
            }
            else if (!targetSlot.IsEmpty())
            {
                InventoryItem itemInSlot = targetSlot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item.title == draggedShopItem.title)
                {
                    BuyItem(draggedShopItem, targetSlot, true);
                }
            }
        }
        draggedShopItem = null;
    }

    private void HandleInventoryItem()
    {
        if (draggedInventoryItem == null) return;
        if (!IsShopObjectUnderMouse()) return;
        Debug.Log("I am here");
        SellItem(draggedInventoryItem);
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
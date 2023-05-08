using ScriptableObjects;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager instance;

   public int maxStackedItems = 4;
   public Slot[] inventorySlots;
   public GameObject inventoryItemPrefab;
   private int selectedSlot = -1;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      ChangeSelectedSlot(0);
      Debug.Log(inventorySlots.Length);
   }

   private void Update()
   {
      if (Input.inputString != null)
      {
         bool isNumber = int.TryParse(Input.inputString, out int number);
         if (isNumber && number > 0 && number < 8)
         {
            ChangeSelectedSlot(number - 1);
         }
      }
   }

   void ChangeSelectedSlot(int newValue)
   {
      if (selectedSlot >= 0)
      {
         inventorySlots[selectedSlot].Deselect();
      }
      
      inventorySlots[newValue].Select();
      selectedSlot = newValue;
   }

   public bool AddLootItems(LootItem items)
   {
      for (int i = 0; i < inventorySlots.Length; i++)
      {
         Slot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && 
             itemInSlot.item == items.item && 
             itemInSlot.count < maxStackedItems && 
             itemInSlot.item.stackable == true)
         {
            if (maxStackedItems < itemInSlot.count + items.count)
            {
               itemInSlot.count = maxStackedItems;
               itemInSlot.RefreshCount();
            }
            else
            {
               itemInSlot.count += items.count;
               itemInSlot.RefreshCount();
               return true;
            }
         }
      }
      
      for (int i = 0; i < inventorySlots.Length; i++)
      {
         Slot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         
         if (itemInSlot == null)
         {
            SpawnNewItem(items, slot);
            return true;
         }
      }

      return false;
   }
   
   public void AddItemToSpecificSlot(Slot slot, Item item)
   {
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot != null && 
          itemInSlot.item == item && 
          itemInSlot.count < maxStackedItems && 
          itemInSlot.item.stackable == true)
      {
         itemInSlot.count++;
         itemInSlot.RefreshCount();
      }
   }

   public bool CheckIfItemInSlot()
   {
      Debug.Log(selectedSlot);
      
      Slot slot = inventorySlots[selectedSlot];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      Debug.Log(itemInSlot.count);
      if (itemInSlot.item != null)
      {
         return true;
      }

      return false;
   }

   public Item GetSelectedItem()
   {
      Slot slot = inventorySlots[selectedSlot];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      Debug.Log("GetSelectedItem" + itemInSlot);
      if (itemInSlot != null)
      {
         Item item = itemInSlot.item;
         Debug.Log(item);
         return item;
      }

      return null;
   }
   
   public void RemoveItem()
   {
      Slot slot = inventorySlots[selectedSlot];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      Debug.Log("RemoveItem" + itemInSlot);
      itemInSlot.count--;
      if (itemInSlot.count <= 0)
      {
         Destroy(itemInSlot.gameObject);
      }
      else 
      { 
         itemInSlot.RefreshCount();
      }
   }

   public void RemoveItem(InventoryItem item)
   {
      Destroy(item.gameObject);
   }

   public void SpawnNewItem(Item item, Slot slot)
   {
      GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
      InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
      inventoryItem.InitialiseItem(item);
   }
   
   public void SpawnNewItem(LootItem item, Slot slot)
   {
      GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
      InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
      inventoryItem.count = item.count;
      inventoryItem.InitialiseItem(item.item);
   }

}

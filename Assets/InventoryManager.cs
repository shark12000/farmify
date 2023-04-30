using System;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager instance;

   public int maxStackedItems = 4;
   public InventorySlot[] inventorySlots;
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
   public bool AddItem(Item item)
   {
      for (int i = 0; i < inventorySlots.Length; i++)
      {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && 
             itemInSlot.item == item && 
             itemInSlot.count < maxStackedItems && 
             itemInSlot.item.stackable == true)
         {
            itemInSlot.count++;
            itemInSlot.RefreshCount();
            return true;
         }
      }
      
      for (int i = 0; i < inventorySlots.Length; i++)
      {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot == null)
         {
            Debug.Log("me here");
            SpawnNewItem(item, slot);
            return true;
         }
      }

      return false;
   }

   public Item GetSelectedItem(bool use)
   {
      InventorySlot slot = inventorySlots[selectedSlot];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot != null)
      {
         Item item = itemInSlot.item;
         if (use == true)
         {
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

         return item;
      }

      return null;
   }

   void SpawnNewItem(Item item, InventorySlot slot)
   {
      GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
      InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
      inventoryItem.IntialiseItem(item);
   }
   
   
}

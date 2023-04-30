using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
   public Item item;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log(item);
         bool canAdd = InventoryManager.instance.AddItem(item);
         Debug.Log(canAdd);
         if (canAdd)
         {
            Debug.Log("Object in the inventory");
            Destroy(this.gameObject);
         }
      }
   }
}

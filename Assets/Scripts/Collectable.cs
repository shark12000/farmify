using System;
using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
   [SerializeField] private SpriteRenderer icon;

   public Item item;

   public void Awake()
   {
      Debug.Log("I am here");
      icon.sprite = item.image;
      Debug.Log(item.stackable);
   }

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

using System;
using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
   [SerializeField] private BoxCollider2D _collider2D;
   [SerializeField] private float moveSpeed;
   public LootItem lootItem;

   public void Awake()
   {
      lootItem = GetComponent<LootItem>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log(lootItem);
         bool canAdd = InventoryManager.instance.AddLootItems(lootItem);
         Debug.Log(canAdd);
         if (canAdd)
         {
            StartCoroutine(MoveAndCollect(other.transform));
            Destroy(this.gameObject);
         }
      }
   }

   private IEnumerator MoveAndCollect(Transform target)
   {
      Destroy(_collider2D);

      while (transform.position != target.position)
      {
         transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
         yield return 0;
      }
      
      Destroy(gameObject);
   }
}
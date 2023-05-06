using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Rubbish
{
   public class Loot : MonoBehaviour
   {
      [SerializeField] private SpriteRenderer sr;
      [SerializeField] private BoxCollider2D _collider2D;
      [SerializeField] private float moveSpeed;
      public LootItem lootItem;
      
      private void OnTriggerEnter2D(Collider2D other)
      {
         if (other.CompareTag("Player"))
         {
            Debug.Log(lootItem);
            bool canAdd = InventoryManager.instance.AddItem(lootItem.item);
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
}

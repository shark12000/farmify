using UnityEngine;


public class Player : MonoBehaviour
{
   private InventoryManager inventory;

   private void Awake()
   {
      inventory = InventoryManager.instance;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Vector3Int position = new Vector3Int((int) transform.position.x, 
            (int) transform.position.y, 0);

         if (GameManager.instance.tileManager.IsInteractable(position))
         {
            Debug.Log("Tile is interactable");
            GameManager.instance.tileManager.SetInteracted(position);
         }
      }
   }
   
   
}

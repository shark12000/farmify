using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
 
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

      if (Input.GetKeyDown(KeyCode.E))
      {
         Vector3Int position = new Vector3Int((int) transform.position.x, 
            (int) transform.position.y, 0);
         
         Vector3Int cellPosition = GameManager.instance.tileManager.interactableMap.WorldToCell(position);
         
         Item item = InventoryManager.instance.GetSelectedItem(true);
         if (item != null && item.data.type == Type.Seed && GameManager.instance.tileManager.isInteracted(position))
         {
            Debug.Log("Is interacted: " + GameManager.instance.tileManager.isInteracted(position));
            Vector3 spawnPosition = GameManager.instance.tileManager.interactableMap.CellToWorld(cellPosition);
            GameObject seed = Instantiate(item.gameObject, spawnPosition, Quaternion.identity);
            GameManager.instance.tileManager.interactableMap.SetTileFlags(cellPosition, TileFlags.None);
            GameManager.instance.tileManager.interactableMap.SetTile(cellPosition, null);
            GameManager.instance.tileManager.interactableMap.RefreshTile(cellPosition);
            seed.transform.parent = GameManager.instance.tileManager.interactableMap.transform;
            Debug.Log("Spawned");
         }
      }
      
      if (Input.GetKeyDown(KeyCode.H))
      {
        Debug.Log(InventoryManager.instance.CheckIfItemInSlot());
      }
   }
   
   
}

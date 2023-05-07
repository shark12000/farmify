using System;
using DataPersistence;
using DataPersistence.Data;
using Rubbish;
using ScriptableObjects;
using Unity.VisualScripting;
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
            GameManager.instance.tileManager.SetInteracted(position);
         }
      }

      if (Input.GetKeyDown(KeyCode.E))
      {
         Vector3Int position = new Vector3Int((int) transform.position.x, 
            (int) transform.position.y, 0);

         var item = InventoryManager.instance.GetSelectedItem();

         if (item != null && GameManager.instance.tileManager.IsInteracted(position))
         {
            Vector3 tilePosition = GameManager.instance.tileManager.ConvertToTilemapPosition(position);
            
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = item.icon;
            Seed seed = (Seed) item;
            bool canPlant = CropManager.instance.PlantCrop(Vector3Int.RoundToInt(tilePosition), seed);
            if (canPlant)
            {
               InventoryManager.instance.RemoveItem();
            }
            else
            {
               Debug.Log("Item cannot be planted here");
            }
             
         }
      }
      
      if (Input.GetKeyDown(KeyCode.H))
      {
        Debug.Log(InventoryManager.instance.CheckIfItemInSlot());
      }

      if (Input.GetKeyDown(KeyCode.Q))
      {
         Vector3Int position = new Vector3Int((int) transform.position.x, 
            (int) transform.position.y, 0);
         
         Vector3 tilePosition = GameManager.instance.tileManager.ConvertToTilemapPosition(position);


         CropManager.instance.HarvestCrop(Vector3Int.RoundToInt(tilePosition));
      }
   }
}

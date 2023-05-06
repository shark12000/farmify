using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rubbish
{
    public class FarmingSystem : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private Tilemap interactableTilemap;
        [SerializeField] private Tilemap tempTilemap;
        [SerializeField] private GameObject lootPrefab;

        private void Seed(Vector3Int position, Item itemToSeed)
        {
            //tempTilemap.SetTile(position, itemToSeed..tile);
        }

        private void Collect(Vector3Int position)
        {
            tempTilemap.SetTile(position, null);

            //TileWithData tile = interactableTilemap.GetTile<TileWithData>(position);
        

            Vector3 pos = interactableTilemap.GetCellCenterWorld(position);
            GameObject loot = Instantiate(lootPrefab, pos, Quaternion.identity);
            //loot.GetComponent<Loot>().Initialise(tile.item);
        }
    }
}

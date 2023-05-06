using UnityEngine;
using UnityEngine.Tilemaps;


public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] public Tilemap tempMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;
    void Start()
    {
        foreach (var position in interactableMap
                     .cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == "interactable_visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        {
            if (tile.name == "interactable")
            {
                return true;
            }
        }

        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

    public bool IsInteracted(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile.name == "Summer_Plowed")
        {
            return true;
        }

        return false;
    }

    public void SetTempMap(Vector3Int position, Tile tile)
    {
        tempMap.SetTile(position, tile);
    }

    public Vector3 ConvertToTilemapPosition(Vector3Int position)
    {
        return interactableMap.CellToWorld(position);
    }


}

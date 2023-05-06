using System.Collections;
using System.Collections.Generic;
using Rubbish;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Crops
{
    public class CropManager : MonoBehaviour
    {
        public Tilemap interactableTilemap;
        public Tilemap tempTilemap;

        public static CropManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            float elapsedGameSeconds = TimeManager.instance.GetSecondsPerGameMinute() * Time.deltaTime;

            foreach (var cell in interactableTilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPosition = new Vector3Int(cell.x, cell.y, cell.z);
                Vector3 position = interactableTilemap.CellToWorld(localPosition);

                Crop crop = GetCropOnTile(localPosition);

                if (crop != null)
                {
                    if (!crop.isHarvested)
                    {
                        crop.timeRemaining -= elapsedGameSeconds;

                        if (crop.timeRemaining <= 0f)
                        {
                            tempTilemap.SetTile(localPosition, crop.type.tiles[^1]);
                            break;
                        }
                        else
                        {
                            float growthPercentage =
                                1f - (crop.timeRemaining / crop.type.timeToHarvest);
                            int growthStage =
                                Mathf.FloorToInt(growthPercentage * crop.type.numGrowthStages);

                            if (growthStage != crop.growthStage)
                            {
                                tempTilemap.SetTile(localPosition, crop.type.tiles[growthStage]);
                                crop.growthStage = growthStage;
                                Debug.Log(crop.growthStage);
                            }
                        }
                    }
                }
            }
        }

        public void HarvestCrop(Vector3Int localPosition)
        {
            if (crops.TryGetValue(localPosition, out Crop crop))
            {
                if (crop.timeRemaining <= 0f)
                {
                    StartCoroutine(HarvestAndDropLoot(localPosition, crop.type.Seedtype));
                }
            }
        }

        private IEnumerator HarvestAndDropLoot(Vector3Int localPosition, TypeOfSeed seedType)
        {
            yield return new WaitForEndOfFrame(); // Wait until the end of the frame to avoid race conditions with Tilemaps

            bool isLooted = LootManager.instance.DropRandomNumberOfLoot(localPosition, seedType);

            if (isLooted)
            {
                crops.Remove(localPosition);
                interactableTilemap.SetTile(localPosition, null);
                tempTilemap.SetTile(localPosition, null);
            }
        }

        public bool PlantCrop(Vector3Int localPosition, Seed cropType)
        {
            if (GetCropOnTile(localPosition) == null)
            {
                Debug.Log(GetCropOnTile(localPosition) == null);
                Debug.Log("Crop was planted");
                Crop crop = new Crop();
                crop.type = cropType;
                crop.timeRemaining = cropType.timeToHarvest;

                tempTilemap.SetTile(localPosition, cropType.tiles[0]);

                crops.Add(localPosition, crop);
                return true;
            }

            return false;
        }

        private Crop GetCropOnTile(Vector3Int localPosition)
        {
            if (crops.TryGetValue(localPosition, out Crop crop))
            {
                return crop;
            }

            return null;
        }

        public bool DetectCrop(Vector3Int localPosition)
        {
            if (crops.TryGetValue(localPosition, out Crop crop))
            {
                return true;
            }

            return false;
        }

        private Dictionary<Vector3Int, Crop> crops = new Dictionary<Vector3Int, Crop>();
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Seed Type", menuName = "Seed Type")]
    public class Seed : Item{
        [Header("Gameplay")]
        public Tile[] tiles; // The sprites for each growth stage of the crop
        public float timeToHarvest; // The time it takes for the crop to be ready for harvest
        public int numGrowthStages;
        public TypeOfSeed Seedtype;
    }
}
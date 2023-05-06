

using ScriptableObjects;

namespace Crops
{
    public class Crop
    {
        public Seed type;
        public int growthStage; 
        public float timeRemaining;
        public bool isHarvested = false;
    }
}

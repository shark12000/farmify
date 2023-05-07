using UnityEngine;

namespace DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        public int coins;
        

        // Update is called once per frame
        public GameData()
        {
            coins = 0;
        }
    }
}

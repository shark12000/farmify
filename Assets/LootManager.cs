using Rubbish;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager instance;
    public GameObject[] lootPrefabs;

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

    public bool DropRandomNumberOfLoot(Vector3 dropLocation, TypeOfSeed typeOfSeed)
    {
        int roll = Random.Range(0, 5);
        
        Vector2 randomOffset = Random.insideUnitCircle * 1f;
        Vector3 dropPosition = dropLocation + new Vector3(randomOffset.x, 0f, randomOffset.y);

        for (int i = 0; i < lootPrefabs.Length; i++)
        {
            LootItem lootItemToUse = lootPrefabs[i].GetComponent<LootItem>();
        
            if (lootItemToUse.type == typeOfSeed)
            {
                GameObject lootObject = Instantiate(lootPrefabs[i], dropPosition, Quaternion.identity);
                LootItem lootItem = lootObject.GetComponent<LootItem>();

                lootItem.type = typeOfSeed;
                lootItem.count = roll;

                return true;
            }
        }
        
        return false;
    }
}
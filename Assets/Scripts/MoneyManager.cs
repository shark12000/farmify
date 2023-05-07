using DataPersistence;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour, IDataPersistence
{
    public Text text;
    public static MoneyManager instance;
    private int coins = 0;
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
        text.text = coins.ToString();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("coins:"+ coins);
        text.text = coins.ToString();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            text.text = coins.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public void LoadData(GameData data)
    {
        coins = data.coins;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = coins;
    }
}

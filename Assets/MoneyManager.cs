using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text text;
    public static MoneyManager instance;
    private int coins = 1000;
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
}

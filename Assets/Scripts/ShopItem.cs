using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [HideInInspector] public Item item;

    [Header("UI")]
    public Image image;

    public Text priceText;

    public void InitialiseItem(Item newItem)
    {
        Debug.Log("Initialise item was called");
        item = newItem;
        
        Debug.Log(item.icon);
        image.sprite = item.icon;
        priceText.text = item.price.ToString();
    }
}
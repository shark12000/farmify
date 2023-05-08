using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    
    [Header("UI")]
    public Image image;

    public Text countText;

    public void RefreshCount()
    {
        Debug.Log("count" + count);
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
        
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = item.icon;
        RefreshCount();
    }
}

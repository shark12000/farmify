using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Type", menuName = "Item Type")]
    public class Item :ScriptableObject
    { 
        public string title;
        public Sprite icon;
        public bool stackable = true;
        public int price;
    }
}

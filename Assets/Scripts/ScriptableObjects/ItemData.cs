using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
   [Header("Only UI")]
   public bool stackable = true;
   [Header("Both")]
   public Sprite image;

   public string itemName = "Item Name";
   public Type type = Type.Unknown;
}

public enum Type
{
   Unknown,
   Seed
}

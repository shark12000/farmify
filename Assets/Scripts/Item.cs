using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    [HideInInspector] public Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public int currentStage = 0; 
    public float growthTimer = 0f; 
    public float growthRate = 1f; 
    public Sprite[] growthSprites;
    private bool isGrown = false;
    
    private void Update()
    {
        if (currentStage < growthSprites.Length - 1)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= growthRate)
            {
                currentStage++;
                Debug.Log(growthTimer);
                GetComponent<SpriteRenderer>().sprite = growthSprites[currentStage];
                growthTimer = 0f;
            }
            else
            {
                isGrown = true;
            }
        }
    }
  
}

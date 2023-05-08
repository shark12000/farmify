using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TileManager tileManager;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>();
    }
}

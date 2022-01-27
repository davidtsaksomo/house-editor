using UnityEngine;

public class FurnitureList : MonoBehaviour
{
    // Singleton
    public static FurnitureList instance;
    [SerializeField]
    public GameObject[] furnitures;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}

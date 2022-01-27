using UnityEngine;

public class FurnitureList : MonoBehaviour
{
    // Singleton
    public static FurnitureList instance;
    [SerializeField]
    public GameObject[] furnitures;

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}

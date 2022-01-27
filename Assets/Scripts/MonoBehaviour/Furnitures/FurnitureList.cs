using UnityEngine;

public class FurnitureList : MonoBehaviour
{
    // Singleton
    public static FurnitureList furnitureList;
    [SerializeField]
    public GameObject[] furnitures;

    void Start()
    {
        if (!furnitureList)
        {
            furnitureList = this;
        }
    }
}

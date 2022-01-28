using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureList : MonoBehaviour
{
    // Singleton
    public static FurnitureList instance;

    [SerializeField]
    FurnitureId[] furnitures = null;
    Dictionary<int, GameObject> furnituresDictionary = new Dictionary<int, GameObject>();

    void Awake()
    {   
        if (!instance)
        {
            instance = this;
        }
        foreach (FurnitureId furniture in furnitures)
        {
            if (!furnituresDictionary.ContainsKey(furniture.id))
            {
                furnituresDictionary.Add(furniture.id, furniture.gameObject);
            }
            else
            {
                Debug.LogWarning("Furnitures: There is a not unique furniture ID");
            }
        }
    }

    public GameObject GetFurniturePrefabByIndex(int index)
    {
        return furnitures[index].gameObject;
    }

    public GameObject GetFurniturePrefabByFurnitureId(int furnitureId)
    {
        if (furnituresDictionary.ContainsKey(furnitureId)) {
            return furnituresDictionary[furnitureId];
        }
        return null;
    }

    public int FurnitureCount
    {
        get => furnitures.Length;
    }
}

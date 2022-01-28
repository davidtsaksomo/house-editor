using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To put on object to manage list of furnitures.
public class FurnitureList : MonoBehaviour
{
    // Singleton
    public static FurnitureList instance;

    // List of all Furniture. Set in inspector
    [Tooltip("List all furnitures here.")]
    [SerializeField]
    FurnitureId[] furnitures = null;

    // Dictionary to make access by furniture id easier.
    Dictionary<int, GameObject> furnituresDictionary = new Dictionary<int, GameObject>();

    void Awake()
    {   
        if (!instance)
        {
            instance = this;
        }

        // populate furniture dictionary
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

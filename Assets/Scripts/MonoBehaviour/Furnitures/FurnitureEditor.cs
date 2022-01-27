using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureEditor : MonoBehaviour
{
    // Singleton
    public static FurnitureEditor instance;

    int selectedFurnitureIndex = 0;
    GameObject selectedFurnitureInstance;
    Collider selectedFurnitureCollider;

    float furnitureRotateMultiplier = 5f;
    Quaternion currentInstanceRotation = Quaternion.identity;

    public bool SelectedFurnitureInstanceActive
    {
        get => selectedFurnitureInstance.activeSelf;
        set => selectedFurnitureInstance.SetActive(value);
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreateFurnitureInstance();
        selectedFurnitureInstance.SetActive(false);
    }

    public void ShowFurnitureOnCursor(Vector3 mousePosition)
    {
        Vector3 mouseTerrainPosition = MouseToWorldPoint.mouseToTerrainPosition(mousePosition);
        Vector3 furniturePosition = new Vector3(mouseTerrainPosition.x, selectedFurnitureCollider.bounds.size.y / 2, mouseTerrainPosition.z);
        selectedFurnitureInstance.transform.position = furniturePosition;
    }

    public void PlaceFurniture(Vector3 mousePosition)
    {
        if (selectedFurnitureInstance.GetComponent<FurniturePrefab>().placeable)
        {
            Instantiate(FurnitureList.instance.furnitures[selectedFurnitureIndex], selectedFurnitureInstance.transform.position, selectedFurnitureInstance.transform.rotation);
        }
    }

    public void RotateFurniture(float deltaY)
    {
        selectedFurnitureInstance.transform.Rotate(new Vector3(0f, deltaY * furnitureRotateMultiplier, 0f));
        currentInstanceRotation = selectedFurnitureInstance.transform.rotation;
    }

    public void CycleFurniture()
    {
        selectedFurnitureIndex = (selectedFurnitureIndex + 1) % FurnitureList.instance.furnitures.Length;
        Destroy(selectedFurnitureInstance);
        CreateFurnitureInstance();
    }

   void CreateFurnitureInstance()
    {
        selectedFurnitureInstance = Instantiate(FurnitureList.instance.furnitures[selectedFurnitureIndex]);
        selectedFurnitureInstance.AddComponent(typeof(FurniturePrefab));
        selectedFurnitureInstance.transform.rotation = currentInstanceRotation;
        selectedFurnitureCollider = selectedFurnitureInstance.GetComponent<Collider>();
    }
}

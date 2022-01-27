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

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        selectedFurnitureInstance = Instantiate(FurnitureList.instance.furnitures[selectedFurnitureIndex]);
        selectedFurnitureCollider = selectedFurnitureInstance.GetComponent<Collider>();
        selectedFurnitureInstance.SetActive(false);
    }

    private void Update()
    {
        if (GameStateManager.instance.gameState == GameState.editingFurniture)
        {
            if (!selectedFurnitureInstance.activeSelf)
            {
                selectedFurnitureInstance.SetActive(true);
            }
            ShowFurnitureOnCursor(Input.mousePosition);
        }
        else if (selectedFurnitureInstance.activeSelf)
        {
            selectedFurnitureInstance.SetActive(false);
        }
    }

    public void ShowFurnitureOnCursor(Vector3 mousePosition)
    {
        Vector3 mouseTerrainPosition = MouseToWorldPoint.mouseToTerrainPosition(mousePosition);
        Vector3 furniturePosition = new Vector3(mouseTerrainPosition.x, selectedFurnitureCollider.bounds.size.y / 2, mouseTerrainPosition.z);
        selectedFurnitureInstance.transform.position = furniturePosition;
    }

    public void PlaceFurniture(Vector3 mousePosition)
    {
        Instantiate(FurnitureList.instance.furnitures[selectedFurnitureIndex], selectedFurnitureInstance.transform.position, selectedFurnitureInstance.transform.rotation);
    }

    public void RotateFurniture(float deltaY)
    {
        selectedFurnitureInstance.transform.Rotate(new Vector3(0f, deltaY * furnitureRotateMultiplier, 0f));
    }

    public void CycleFurniture()
    {
        selectedFurnitureIndex = (selectedFurnitureIndex + 1) % FurnitureList.instance.furnitures.Length;
        Destroy(selectedFurnitureInstance);
        selectedFurnitureInstance = Instantiate(FurnitureList.instance.furnitures[selectedFurnitureIndex]);
        selectedFurnitureCollider = selectedFurnitureInstance.GetComponent<Collider>();
    }
}

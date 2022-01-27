using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureEditor : MonoBehaviour
{
    // Singleton
    public static FurnitureEditor instance;

    GameObject selectedFurniture;
    GameObject selectedFurnitureInstance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        selectedFurniture = FurnitureList.instance.furnitures[1];
        selectedFurnitureInstance = Instantiate(selectedFurniture);
    }

    public void ShowFurnitureOnCursor(Vector3 mousePosition)
    {
        Vector3 mouseTerrainPosition = MouseToWorldPoint.mouseToTerrainPosition(mousePosition);
        Vector3 furniturePosition = new Vector3(mouseTerrainPosition.x, selectedFurnitureInstance.GetComponent<Collider>().bounds.size.y / 2, mouseTerrainPosition.z);
        selectedFurnitureInstance.transform.position = furniturePosition;
    }

    public void PlaceFurniture()
    {
        Vector3 mouseTerrainPosition = MouseToWorldPoint.mouseToTerrainPosition(Input.mousePosition);
        Vector3 furniturePosition = new Vector3(mouseTerrainPosition.x, selectedFurnitureInstance.GetComponent<Collider>().bounds.size.y / 2, mouseTerrainPosition.z);
        Instantiate(selectedFurniture, furniturePosition, Quaternion.identity);
    }
}

﻿using System.Collections;
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

    [SerializeField]
    Transform furnitureParent = null;

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
        if (selectedFurnitureInstance.GetComponent<FurnitureCursorInstance>().placeable)
        {
            GameObject placedFurniture = Instantiate(FurnitureList.instance.GetFurniturePrefabByIndex(selectedFurnitureIndex), selectedFurnitureInstance.transform.position, selectedFurnitureInstance.transform.rotation, furnitureParent);
            GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Add(placedFurniture.GetInstanceID(), new PlacedFurniture(placedFurniture.GetComponent<FurnitureId>().id, placedFurniture.transform, GameConstants.DefaultFurnitureColor));
        }
    }

    public void DeleteFurniture(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(GameConstants.furnitureName)))
        {
            Destroy(hitData.collider.gameObject);
            FurnitureId furnitureId = hitData.collider.GetComponent<FurnitureId>();
            if (furnitureId)
            {
                GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Remove(furnitureId.id);
            }
        }
    }

    public void RotateFurniture(float deltaY)
    {
        selectedFurnitureInstance.transform.Rotate(new Vector3(0f, deltaY * furnitureRotateMultiplier, 0f));
        currentInstanceRotation = selectedFurnitureInstance.transform.rotation;
    }

    public void CycleFurniture()
    {
        selectedFurnitureIndex = (selectedFurnitureIndex + 1) % FurnitureList.instance.FurnitureCount;
        ReloadFurnitureInstance();
    }

   void CreateFurnitureInstance()
    {
        selectedFurnitureInstance = Instantiate(FurnitureList.instance.GetFurniturePrefabByIndex(selectedFurnitureIndex));
        selectedFurnitureInstance.AddComponent(typeof(FurnitureCursorInstance));
        selectedFurnitureInstance.transform.rotation = currentInstanceRotation;
        selectedFurnitureCollider = selectedFurnitureInstance.GetComponent<Collider>();
    }

    public void ReloadFurnitureInstance()
    {
        Destroy(selectedFurnitureInstance);
        CreateFurnitureInstance();
    }

    public void SpawnFurnitureFromData()
    {
        DestroyAll();
    }

    void DestroyAll()
    {
        foreach (Transform child in furnitureParent)
        {
            Destroy(child.gameObject);
        }
    }
}

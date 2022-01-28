using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage furniture edit feature
public class FurnitureEditor : MonoBehaviour
{
    // Singleton
    public static FurnitureEditor instance;

    // Current selected furniture index
    int selectedFurnitureIndex = 0;

    // Selected furniture instance to be shown at mouse cursor
    GameObject selectedFurnitureInstance;

    // Selected furniture collider to calculate object bounds
    Collider selectedFurnitureCollider;

    // Furniture rotate speed multiplier
    float furnitureRotateMultiplier = 5f;

    // Current selected furniture rotation
    Quaternion currentInstanceRotation = Quaternion.identity;

    // Parent object to instantiate furniture object to. Set in the inspector.
    [Tooltip("Parent object to instantiate furniture object to.")]
    [SerializeField]
    Transform furnitureParent = null;

    // Is cursor not outside the land area
    bool cursorPositionValid = false;

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
        cursorPositionValid = !float.IsInfinity(furniturePosition.x) && !float.IsInfinity(furniturePosition.z);
        if (cursorPositionValid)
        {
            selectedFurnitureInstance.transform.position = furniturePosition;
        }
        
    }

    public void PlaceFurniture(Vector3 mousePosition)
    {
        if (selectedFurnitureInstance.GetComponent<FurnitureCursorInstance>().placeable && cursorPositionValid)
        {
            // Place furniture
            GameObject placedFurniture = Instantiate(FurnitureList.instance.GetFurniturePrefabByIndex(selectedFurnitureIndex), selectedFurnitureInstance.transform.position, selectedFurnitureInstance.transform.rotation, furnitureParent);
            // Update placed furniture data
            GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Add(placedFurniture.GetInstanceID(), new PlacedFurniture(placedFurniture.GetComponent<FurnitureId>().id, placedFurniture.transform, GameConstants.DefaultFurnitureColor));
        }
    }

    public void DeleteFurniture(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(GameConstants.furnitureName)))
        {
            // Update placed furniture data
            GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Remove(hitData.collider.gameObject.GetInstanceID());
            // Delete furniture
            Destroy(hitData.collider.gameObject);
        }
    }

    public void RotateFurniture(float deltaY)
    {
        selectedFurnitureInstance.transform.Rotate(new Vector3(0f, deltaY * furnitureRotateMultiplier, 0f));
        currentInstanceRotation = selectedFurnitureInstance.transform.rotation;
    }

    // Change furniture selection
    public void CycleFurniture()
    {
        selectedFurnitureIndex = (selectedFurnitureIndex + 1) % FurnitureList.instance.FurnitureCount;
        ReloadFurnitureInstance();
    }

    // Create instance of selected furniture
    void CreateFurnitureInstance()
    {
        selectedFurnitureInstance = Instantiate(FurnitureList.instance.GetFurniturePrefabByIndex(selectedFurnitureIndex));
        selectedFurnitureInstance.AddComponent(typeof(FurnitureCursorInstance));
        selectedFurnitureInstance.transform.rotation = currentInstanceRotation;
        selectedFurnitureCollider = selectedFurnitureInstance.GetComponent<Collider>();
    }

    // Reload instance of selected furniture
    public void ReloadFurnitureInstance()
    {
        Destroy(selectedFurnitureInstance);
        CreateFurnitureInstance();
    }

    // Delete all furniture and populate new furnitures from furniture data
    public void SpawnFurnitureFromData()
    {
        DestroyAll();
        // Copy so that we can change the dictionary while looping it
        Dictionary<int, PlacedFurniture> dictionaryCopy = new Dictionary<int, PlacedFurniture>(GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures);

        foreach (KeyValuePair<int, PlacedFurniture> furnitureKeyValuePair in dictionaryCopy)
        {
            PlacedFurniture furnitureData = furnitureKeyValuePair.Value;
            GameObject furniturePrefab = FurnitureList.instance.GetFurniturePrefabByFurnitureId(furnitureData.furnitureId);
            if (furniturePrefab)
            {
                // Place furniture
                GameObject furniture = Instantiate(furniturePrefab, new Vector3(furnitureData.position[0], furnitureData.position[1], furnitureData.position[2]), Quaternion.Euler(new Vector3(0f, furnitureData.yrotation, 0f)), furnitureParent);
                
                // Update game object instance id
                GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Add(furniture.GetInstanceID(), furnitureData);
                GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.Remove(furnitureKeyValuePair.Key);

                // Set appropriate color
                MeshRenderer meshRenderer = furniture.GetComponent<MeshRenderer>();
                if (meshRenderer)
                {
                    meshRenderer.material.color = new Color(furnitureData.color[0], furnitureData.color[1], furnitureData.color[2]);
                }
            }
        }
    }

    void DestroyAll()
    {
        foreach (Transform child in furnitureParent)
        {
            Destroy(child.gameObject);
        }
    }
}

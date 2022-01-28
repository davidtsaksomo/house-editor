using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Singleton
    public static ColorChanger instance;

    int selectedColorIndex = 0;

    [SerializeField]
    Color[] colors = null;

    WallData WallData
    {
        get => GameDataManager.instance.gameData.wallData;
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
        ColorSelectionIndicator.instance.SetColor(colors[selectedColorIndex]);
    }

    public void ChangeObjectColor(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(GameConstants.furnitureName) | 1 << LayerMask.NameToLayer(GameConstants.wallName)))
        {
            GameObject targetObject = hitData.collider.gameObject;
            MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
            if (!meshRenderer) 
            {
                return; 
            }
            meshRenderer.material.color = colors[selectedColorIndex];

            if (targetObject.CompareTag(GameConstants.wallName))
            {
                if ((int)Mathf.Round(targetObject.transform.eulerAngles.y) == 0)
                {
                    int x = (int)Mathf.Floor(targetObject.transform.position.x);
                    int z = (int)Mathf.Round(targetObject.transform.position.z);
                    if (z >= 1)
                    {
                        z -= 1;
                        WallData.wallUnits[x, z].Top.SetColor(colors[selectedColorIndex]);
                    }
                    else
                    {
                        WallData.wallUnits[x, z].Bottom.SetColor(colors[selectedColorIndex]);
                    }
                }
                else
                {
                    int x = (int)Mathf.Round(targetObject.transform.position.x);
                    int z = (int)Mathf.Floor(targetObject.transform.position.z);
                    if (x >= 1)
                    {
                        x -= 1;
                        WallData.wallUnits[x, z].Right.SetColor(colors[selectedColorIndex]);
                    }
                    else
                    {
                        WallData.wallUnits[x, z].Left.SetColor(colors[selectedColorIndex]);
                    }
                }
            }
            else if (targetObject.CompareTag(GameConstants.furnitureName))
            {
                int instanceId = targetObject.GetInstanceID();
                if (GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures.ContainsKey(instanceId))
                {
                    GameDataManager.instance.gameData.placedFurniturelist.placedFurnitures[instanceId].ChangeColor(colors[selectedColorIndex]);
                }
            }
        }
    }

    public void ChangeColorSelection()
    {
        selectedColorIndex = (selectedColorIndex + 1) % colors.Length;
        ColorSelectionIndicator.instance.SetColor(colors[selectedColorIndex]);
    }
}

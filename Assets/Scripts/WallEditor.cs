using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    // Singleton instance
    public static WallEditor wallEditor;

    [SerializeField]
    GameObject wallPrefab = null;
    [SerializeField]
    GameObject wallParent = null;

    WallData wallData = new WallData(GameConfiguration.worldWidth, GameConfiguration.worldLength);

    TerrainCollider terrainCollider;

    float clickMaxDistance = 0.1f;

    private void Start()
    {
        if (!wallEditor)
        {
            wallEditor = this;
        }

        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
    }

    public void AddWall(Vector3 mousePosition)
    {
        // Get position on the grid
        Vector3 worldPosition = getWorldMousePosition(mousePosition);
        Vector3 roundedPosition = new Vector3(Mathf.Round(worldPosition.x), 0f, Mathf.Round(worldPosition.z));
        Vector3 gridPosition = new Vector3(Mathf.Floor(worldPosition.x), 0f, Mathf.Floor(worldPosition.z));

        float xDistanceToGridLine = Mathf.Abs(worldPosition.x - roundedPosition.x);
        float zDistanceToGridLine = Mathf.Abs(worldPosition.z - roundedPosition.z);

        float xDistanceToGridLineCenter = Mathf.Abs(worldPosition.x - (gridPosition.x + 0.5f));
        float zDistanceToGridLineCenter = Mathf.Abs(worldPosition.z - (gridPosition.z + 0.5f));

        if (xDistanceToGridLine < zDistanceToGridLine && zDistanceToGridLineCenter < clickMaxDistance) // wall on left or right
        {
            Vector3 instancePosition = new Vector3(roundedPosition.x, (wallPrefab.transform.localScale.y / 2), gridPosition.z + 0.5f);

            if (roundedPosition.x <= worldPosition.x) // wall on the left
            {
                if (gridPosition.x >= 1)
                {
                    if (!wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right)
                    {
                        wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right = true;
                        Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }

                }
                else
                {
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left = true;
                        Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }
                }

            }
            else // wall on the right
            {
                if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right = true;
                    Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                }
            }


        }
        else if (xDistanceToGridLine > zDistanceToGridLine && xDistanceToGridLineCenter < clickMaxDistance) // wall on top or bottom
        {
            Vector3 instancePosition = new Vector3(gridPosition.x + 0.5f, (wallPrefab.transform.localScale.y / 2), roundedPosition.z);

            if (roundedPosition.z <= worldPosition.z) // wall on the bottom
            {
                if (gridPosition.z >= 1)
                {
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top = true;
                        Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
                else
                {
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom = true;
                        Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
            }
            else // wall on the top
            {
                if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top = true;
                    Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                }
            }
        }
    }

    public void RemoveWall(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        GameObject removedWall;
        
        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer("Wall")))
        {
            removedWall = hitData.collider.gameObject;

            if ((int)Mathf.Round(removedWall.transform.eulerAngles.y) == 0)
            {
                int x = (int)Mathf.Floor(removedWall.transform.position.x);
                int z = (int)Mathf.Round(removedWall.transform.position.z);
                if (z >= 1)
                {
                    z -= 1;
                    wallData.wallUnits[x, z].Top = false;
                }
                else
                {
                    wallData.wallUnits[x, z].Bottom = false;
                }
            }
            else
            {
                int x = (int)Mathf.Round(removedWall.transform.position.x);
                int z = (int)Mathf.Floor(removedWall.transform.position.z);
                if (x >= 1)
                {
                    x -= 1;
                    wallData.wallUnits[x, z].Right = false;
                }
                else
                {
                    wallData.wallUnits[x, z].Left = false;
                }
            }
            Destroy(removedWall);
        }
    }

    Vector3 getWorldMousePosition(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (terrainCollider.Raycast(ray, out RaycastHit hitData, 1000))
        {
            return hitData.point;
        }
        return Vector3.positiveInfinity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    // Singleton instance
    public static WallEditor instance;

    [SerializeField]
    GameObject wallPrefab = null;
    [SerializeField]
    GameObject wallParent = null;

    WallData wallData;

    float clickMaxDistance = 0.1f;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        wallData = GameData.wallData;
    }

    public void AddWall(Vector3 mousePosition)
    {
        // Get position on the grid
        Vector3 worldPosition = MouseToWorldPoint.mouseToTerrainPosition(mousePosition);
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
                    if (!wallData.walls[(int)gridPosition.x - 1, (int)gridPosition.z].wallUnit.Right)
                    {
                        wallData.walls[(int)gridPosition.x - 1, (int)gridPosition.z].wallUnit.Right = true;
                        ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }

                }
                else
                {
                    if (!wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Left)
                    {
                        wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Left = true;
                        ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }
                }

            }
            else // wall on the right
            {
                if (!wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Right)
                {
                    wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Right = true;
                    ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
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
                    if (!wallData.walls[(int)gridPosition.x, (int)gridPosition.z - 1].wallUnit.Top)
                    {
                        wallData.walls[(int)gridPosition.x, (int)gridPosition.z - 1].wallUnit.Top = true;
                        ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
                else
                {
                    if (!wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Bottom)
                    {
                        wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Bottom = true;
                        ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
            }
            else // wall on the top
            {
                if (!wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Top)
                {
                    wallData.walls[(int)gridPosition.x, (int)gridPosition.z].wallUnit.Top = true;
                    ObjectPooler.instance.SpawnFromPool("Wall", instancePosition, Quaternion.identity, wallParent.transform);
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
                    wallData.walls[x, z].wallUnit.Top = false;
                    wallData.walls[x, z].wallPropUnit.Top = null;
                }
                else
                {
                    wallData.walls[x, z].wallUnit.Bottom = false;
                    wallData.walls[x, z].wallPropUnit.Bottom = null;
                }
            }
            else
            {
                int x = (int)Mathf.Round(removedWall.transform.position.x);
                int z = (int)Mathf.Floor(removedWall.transform.position.z);
                if (x >= 1)
                {
                    x -= 1;
                    wallData.walls[x, z].wallUnit.Right = false;
                    wallData.walls[x, z].wallPropUnit.Right = null;
                }
                else
                {
                    wallData.walls[x, z].wallUnit.Left = false;
                    wallData.walls[x, z].wallPropUnit.Left = null;
                }
            }
            ObjectPooler.instance.DespawnToPool("Wall", removedWall);
        }
    }
}
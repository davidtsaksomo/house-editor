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

    float clickMaxDistance = 0.05f;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        wallData = GameDataManager.instance.gameData.wallData;
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
                    if (!wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right.exist)
                    {
                        wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right.exist = true;
                        ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }

                }
                else
                {
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left.exist)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left.exist = true;
                        ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    }
                }

            }
            else // wall on the right
            {
                if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right.exist)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right.exist = true;
                    ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
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
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top.exist)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top.exist = true;
                        ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
                else
                {
                    if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom.exist)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom.exist = true;
                        ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.identity, wallParent.transform);
                    }
                }
            }
            else // wall on the top
            {
                if (!wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top.exist)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top.exist = true;
                    ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, instancePosition, Quaternion.identity, wallParent.transform);
                }
            }
        }
    }

    public void RemoveWall(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        GameObject removedWall;
        
        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(GameConstants.wallName)))
        {
            removedWall = hitData.collider.gameObject;

            if ((int)Mathf.Round(removedWall.transform.eulerAngles.y) == 0)
            {
                int x = (int)Mathf.Floor(removedWall.transform.position.x);
                int z = (int)Mathf.Round(removedWall.transform.position.z);
                if (z >= 1)
                {
                    z -= 1;
                    wallData.wallUnits[x, z].Top.exist = false;
                    wallData.wallUnits[x, z].Top.wallProp = null;
                }
                else
                {
                    wallData.wallUnits[x, z].Bottom.exist = false;
                    wallData.wallUnits[x, z].Bottom.wallProp = null;
                }
            }
            else
            {
                int x = (int)Mathf.Round(removedWall.transform.position.x);
                int z = (int)Mathf.Floor(removedWall.transform.position.z);
                if (x >= 1)
                {
                    x -= 1;
                    wallData.wallUnits[x, z].Right.exist = false;
                    wallData.wallUnits[x, z].Right.wallProp = null;
                }
                else
                {
                    wallData.wallUnits[x, z].Left.exist = false;
                    wallData.wallUnits[x, z].Left.wallProp = null;
                }
            }
            ObjectPooler.instance.DespawnToPool(GameConstants.wallName, removedWall);
        }
    }

    public void SpawnWallFromData()
    {
        DestroyAll();

        IWallUnit[,] wallUnits = GameDataManager.instance.gameData.wallData.wallUnits;
        for (int x = 0; x < GameConstants.worldWidth; x++)
        {
            for (int z = 0; z < GameConstants.worldLength; z++)
            {
                if (wallUnits[x, z].Top.exist)
                {
                    GameObject wall = ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, new Vector3(x + 0.5f, (wallPrefab.transform.localScale.y / 2), z + 1f), Quaternion.identity, wallParent.transform);

                    if (wallUnits[x, z].Top.wallProp != null && wallUnits[x, z].Top.wallProp.Name == GameConstants.doorName)
                    {
                        ObjectPooler.instance.SpawnFromPool(GameConstants.doorName, wall.transform.position, wall.transform.rotation, wall.transform);
                    }
                }

                if (wallUnits[x, z].Right.exist)
                {
                    GameObject wall = ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, new Vector3(x + 1f, (wallPrefab.transform.localScale.y / 2), z + 0.5f), Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);

                    if (wallUnits[x, z].Right.wallProp != null && wallUnits[x, z].Right.wallProp.Name == GameConstants.doorName)
                    {
                        ObjectPooler.instance.SpawnFromPool(GameConstants.doorName, wall.transform.position, wall.transform.rotation, wall.transform);
                    }
                }

                if (z == 0 && wallUnits[x, z].Bottom.exist)
                {
                    GameObject wall = ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, new Vector3(x + 0.5f, (wallPrefab.transform.localScale.y / 2), z), Quaternion.identity, wallParent.transform);

                    if (wallUnits[x, z].Bottom.wallProp != null && wallUnits[x, z].Bottom.wallProp.Name == GameConstants.doorName)
                    {
                        ObjectPooler.instance.SpawnFromPool(GameConstants.doorName, wall.transform.position, wall.transform.rotation, wall.transform);
                    }
                }

                if (x == 0 && wallUnits[x, z].Left.exist)
                {
                    GameObject wall = ObjectPooler.instance.SpawnFromPool(GameConstants.wallName, new Vector3(x, (wallPrefab.transform.localScale.y / 2), z + 0.5f), Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);

                    if (wallUnits[x, z].Left.wallProp != null && wallUnits[x, z].Left.wallProp.Name == GameConstants.doorName)
                    {
                        ObjectPooler.instance.SpawnFromPool(GameConstants.doorName, wall.transform.position, wall.transform.rotation, wall.transform);
                    }
                }
            }
        }
    }

    private void DestroyAll()
    {
        foreach (Transform child in wallParent.transform)
        {
            ObjectPooler.instance.DespawnToPool(GameConstants.wallName, child.gameObject);
        }
    }
}
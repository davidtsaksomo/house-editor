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

    GameObject[,,] wallInstance; // 3rd axis: 0 = top, 1 = right, 2 = bottom, 3 = left

    float clickMaxDistance = 0.2f;

    private void Start()
    {
        if (!wallEditor)
        {
            wallEditor = this;
        }

        wallInstance = new GameObject[GameConfiguration.worldWidth, GameConfiguration.worldLength, 4];
    }

    public void AddWall(Vector3 worldPosition)
    {
        EditWall(worldPosition);
    }

    public void RemoveWall(Vector3 worldPosition)
    {
        EditWall(worldPosition, true);
    }

    void EditWall(Vector3 worldPosition, bool remove = false)
    {
        // Get position on the grid
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
                    if (!remove && !wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right)
                    {
                        wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right = true;
                        GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                        wallInstance[(int)gridPosition.x - 1, (int)gridPosition.z, 1] = wall;
                    }
                    else if (remove && wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right)
                    {
                        wallData.wallUnits[(int)gridPosition.x - 1, (int)gridPosition.z].Right = false;
                        Destroy(wallInstance[(int)gridPosition.x - 1, (int)gridPosition.z, 1]);
                    }

                }
                else
                {
                    if (!remove && !wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left = true;
                        GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                        wallInstance[(int)gridPosition.x, (int)gridPosition.z, 3] = wall;
                    }
                    else if (remove && wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Left = false;
                        Destroy(wallInstance[(int)gridPosition.x, (int)gridPosition.z, 3]);
                    }
                }

            }
            else // wall on the right
            {
                if (!remove && !wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right = true;
                    GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
                    wallInstance[(int)gridPosition.x, (int)gridPosition.z, 1] = wall;
                }
                else if (remove && wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Right = false;
                    Destroy(wallInstance[(int)gridPosition.x, (int)gridPosition.z, 1]);
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
                    if (!remove && !wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top = true;
                        GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                        wallInstance[(int)gridPosition.x, (int)gridPosition.z - 1, 0] = wall;
                    }
                    else if (remove && wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z - 1].Top = false;
                        Destroy(wallInstance[(int)gridPosition.x, (int)gridPosition.z - 1, 0]);
                    }
                }
                else
                {
                    if (!remove && !wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom = true;
                        GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                        wallInstance[(int)gridPosition.x, (int)gridPosition.z, 2] = wall;
                    }
                    else if (remove && wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom)
                    {
                        wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Bottom = false;
                        Destroy(wallInstance[(int)gridPosition.x, (int)gridPosition.z, 2]);
                    }
                }
            }
            else // wall on the top
            {
                if (!remove && !wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top = true;
                    GameObject wall = Instantiate(wallPrefab, instancePosition, Quaternion.identity, wallParent.transform);
                    wallInstance[(int)gridPosition.x, (int)gridPosition.z, 0] = wall;
                }
                else if (remove && wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top)
                {
                    wallData.wallUnits[(int)gridPosition.x, (int)gridPosition.z].Top = false;
                    Destroy(wallInstance[(int)gridPosition.x, (int)gridPosition.z, 0]);
                }
            }
        }
    }
}

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

    private void Awake()
    {
        if (!wallEditor)
        {
            wallEditor = this;
        }
    }

    public void AddWall(Vector3 worldPosition)
    {
        Vector3 gridPosition = new Vector3(Mathf.Round(worldPosition.x), 0f, Mathf.Round(worldPosition.z));

        if (Mathf.Abs(gridPosition.x - worldPosition.x) < Mathf.Abs(gridPosition.z - worldPosition.z))
        {
            Instantiate(wallPrefab, new Vector3(gridPosition.x, gridPosition.y, Mathf.Floor(worldPosition.z) + 0.5f), Quaternion.Euler(new Vector3(0, 90, 0)), wallParent.transform);
        }
        else
        {
            Instantiate(wallPrefab, new Vector3(Mathf.Floor(worldPosition.x) + 0.5f, gridPosition.y, gridPosition.z), Quaternion.identity, wallParent.transform);
        }
    }
}

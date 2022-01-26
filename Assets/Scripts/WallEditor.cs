using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    // Singleton instance
    public static WallEditor wallEditor;

    [SerializeField]
    GameObject wallPrefab = null;

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
        Instantiate(wallPrefab, gridPosition, Quaternion.identity);
        print(gridPosition);
    }
}

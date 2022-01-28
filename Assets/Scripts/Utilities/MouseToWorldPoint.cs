using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWorldPoint
{
    // Convert mouse position to world position on main main terrain
    public static Vector3 mouseToTerrainPosition(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Terrain.activeTerrain.GetComponent<TerrainCollider>().Raycast(ray, out RaycastHit hitData, 1000))
        {
            return hitData.point;
        }
        return Vector3.positiveInfinity;
    }
}

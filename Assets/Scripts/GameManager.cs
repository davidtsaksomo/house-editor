using UnityEngine;

public class GameManager : MonoBehaviour
{
    TerrainCollider terrainCollider;

    void Start()
    {
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            WallEditor.wallEditor.AddWall(getMousePosition());
        }
        else if (Input.GetMouseButton(1))
        {
            WallEditor.wallEditor.RemoveWall(getMousePosition());
        }
    }

    Vector3 getMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (terrainCollider.Raycast(ray, out hitData, 1000))
        {
            return hitData.point;
        }
        return Vector3.positiveInfinity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEditor : MonoBehaviour
{
    // Singleton instance
    public static DoorEditor doorEditor;
    [SerializeField]
    GameObject doorPrefab = null;

    private void Start()
    {
        if (!doorEditor)
        {
            doorEditor = this;
        }
    }

    enum Position
    {
        top,
        right,
        bottom,
        left
    }

    public void AddDoor(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        GameObject targetWall;

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer("Wall")))
        {
            targetWall = hitData.collider.gameObject;
            WallData wallData = GameData.wallData;
            int x;
            int z;
            Position position;
            if ((int)Mathf.Round(targetWall.transform.eulerAngles.y) == 0)
            {
                x = (int)Mathf.Floor(targetWall.transform.position.x);
                z = (int)Mathf.Round(targetWall.transform.position.z);
                if (z >= 1)
                {
                    z -= 1;
                    position = Position.top;
                }
                else
                {
                    position = Position.bottom;
                }
            }
            else
            {
                x = (int)Mathf.Round(targetWall.transform.position.x);
                z = (int)Mathf.Floor(targetWall.transform.position.z);
                if (x >= 1)
                {
                    x -= 1;
                    position = Position.right;
                }
                else
                {
                    position = Position.left;
                }
            }

            switch (position)
            {
                case Position.top:
                    if (wallData.walls[x, z].wallPropUnit.Top == null) 
                    { 
                        wallData.walls[x, z].wallPropUnit.Top = new Door(); 
                    }
                    else
                    {
                        return;
                    }
                    break;
                case Position.right:
                    if (wallData.walls[x, z].wallPropUnit.Right == null)
                    {
                        wallData.walls[x, z].wallPropUnit.Right = new Door();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case Position.bottom:
                    if (wallData.walls[x, z].wallPropUnit.Bottom == null)
                    {
                        wallData.walls[x, z].wallPropUnit.Bottom = new Door();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case Position.left:
                    if (wallData.walls[x, z].wallPropUnit.Left == null)
                    {
                        wallData.walls[x, z].wallPropUnit.Left = new Door();
                    }
                    else
                    {
                        return;
                    }
                    break;
            }

            GameObject door = Instantiate(doorPrefab, targetWall.transform.position, targetWall.transform.rotation, targetWall.transform);
            // Adjust local scale
            door.transform.localScale = new Vector3(door.transform.localScale.x / targetWall.transform.localScale.x, door.transform.localScale.y / targetWall.transform.localScale.y, door.transform.localScale.z / targetWall.transform.localScale.z);
        }
    }
}

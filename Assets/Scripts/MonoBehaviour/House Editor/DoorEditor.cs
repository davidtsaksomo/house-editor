using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEditor : MonoBehaviour
{
    // Singleton instance
    public static DoorEditor instance;

    enum Position
    {
        top,
        right,
        bottom,
        left
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void AddDoor(Vector3 mousePosition, bool delete = false)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        GameObject targetObject;

        string layerMaskName = delete ? "Door" : "Wall";

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(layerMaskName)))
        {
            targetObject = hitData.collider.gameObject;
            Transform wallTransform = delete ? targetObject.transform.parent : targetObject.transform;
            WallData wallData = GameData.wallData;
            int x;
            int z;
            Position position;

            if ((int)Mathf.Round(wallTransform.eulerAngles.y) == 0)
            {
                x = (int)Mathf.Floor(wallTransform.position.x);
                z = (int)Mathf.Round(wallTransform.position.z);
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
                x = (int)Mathf.Round(wallTransform.position.x);
                z = (int)Mathf.Floor(wallTransform.position.z);
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

            if (!delete)
            {

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

                GameObject door = ObjectPooler.instance.SpawnFromPool("Door", wallTransform.position, wallTransform.rotation, wallTransform);
                // Adjust local scale
                door.transform.localScale = new Vector3(door.transform.localScale.x / wallTransform.localScale.x, door.transform.localScale.y / wallTransform.localScale.y, door.transform.localScale.z / wallTransform.localScale.z);
            }
            else
            {
                switch (position)
                {
                    case Position.top:
                        wallData.walls[x, z].wallPropUnit.Top = null;
                        break;
                    case Position.right:
                        wallData.walls[x, z].wallPropUnit.Right = null;
                        break;
                    case Position.bottom:
                        wallData.walls[x, z].wallPropUnit.Bottom = null;
                        break;
                    case Position.left:
                        wallData.walls[x, z].wallPropUnit.Left = null;
                        break;
                }
                ObjectPooler.instance.DespawnToPool("Door", targetObject);
            }
        }
    }
}

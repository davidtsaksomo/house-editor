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
            WallData wallData = GameDataManager.instance.gameData.wallData;
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
                        if (wallData.wallUnits[x, z].Top.wallProp == null)
                        {
                            wallData.wallUnits[x, z].Top.wallProp = new Door();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case Position.right:
                        if (wallData.wallUnits[x, z].Right.wallProp == null)
                        {
                            wallData.wallUnits[x, z].Right.wallProp = new Door();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case Position.bottom:
                        if (wallData.wallUnits[x, z].Bottom.wallProp == null)
                        {
                            wallData.wallUnits[x, z].Bottom.wallProp = new Door();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case Position.left:
                        if (wallData.wallUnits[x, z].Left.wallProp == null)
                        {
                            wallData.wallUnits[x, z].Left.wallProp = new Door();
                        }
                        else
                        {
                            return;
                        }
                        break;
                }

                ObjectPooler.instance.SpawnFromPool("Door", wallTransform.position, wallTransform.rotation, wallTransform);
            }
            else
            {
                switch (position)
                {
                    case Position.top:
                        wallData.wallUnits[x, z].Top.wallProp = null;
                        break;
                    case Position.right:
                        wallData.wallUnits[x, z].Right.wallProp = null;
                        break;
                    case Position.bottom:
                        wallData.wallUnits[x, z].Bottom.wallProp = null;
                        break;
                    case Position.left:
                        wallData.wallUnits[x, z].Left.wallProp = null;
                        break;
                }
                ObjectPooler.instance.DespawnToPool("Door", targetObject);
            }
        }
    }
}

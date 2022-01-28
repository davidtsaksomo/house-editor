using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage door editing feature
public class DoorEditor : MonoBehaviour
{
    // Singleton instance
    public static DoorEditor instance;

    public enum DoorPosition
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

    public void EditDoor(Vector3 mousePosition, bool delete = false)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        GameObject targetObject;

        string layerMaskName = delete ? GameConstants.doorName : GameConstants.wallName;

        // Use ray to get target object
        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer(layerMaskName)))
        {
            targetObject = hitData.collider.gameObject;
            Transform wallTransform = delete ? targetObject.transform.parent : targetObject.transform;
            int x;
            int z;
            DoorPosition position;

            // Find object x and z, and door position
            if ((int)Mathf.Round(wallTransform.eulerAngles.y) == 0)
            {
                x = (int)Mathf.Floor(wallTransform.position.x);
                z = (int)Mathf.Round(wallTransform.position.z);
                if (z >= 1)
                {
                    z -= 1;
                    position = DoorPosition.top;
                }
                else
                {
                    position = DoorPosition.bottom;
                }
            }
            else
            {
                x = (int)Mathf.Round(wallTransform.position.x);
                z = (int)Mathf.Floor(wallTransform.position.z);
                if (x >= 1)
                {
                    x -= 1;
                    position = DoorPosition.right;
                }
                else
                {
                    position = DoorPosition.left;
                }
            }

            // Create or delete
            if (!delete)
            {
                AddDoor(wallTransform, x, z, position);
            }
            else
            {
                DeleteDoor(targetObject, x, z, position);
            }
        }
    }

    void AddDoor(Transform wall, int x, int z, DoorPosition position)
    {
        // Update door information on wall data
        WallData wallData = GameDataManager.instance.gameData.wallData;
        switch (position)
        {
            case DoorPosition.top:
                if (wallData.wallUnits[x, z].Top.wallProp == null)
                {
                    wallData.wallUnits[x, z].Top.wallProp = new Door();
                }
                else
                {
                    return;
                }
                break;
            case DoorPosition.right:
                if (wallData.wallUnits[x, z].Right.wallProp == null)
                {
                    wallData.wallUnits[x, z].Right.wallProp = new Door();
                }
                else
                {
                    return;
                }
                break;
            case DoorPosition.bottom:
                if (wallData.wallUnits[x, z].Bottom.wallProp == null)
                {
                    wallData.wallUnits[x, z].Bottom.wallProp = new Door();
                }
                else
                {
                    return;
                }
                break;
            case DoorPosition.left:
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
        // Spawn door
        ObjectPooler.instance.SpawnFromPool(GameConstants.doorName, wall.position, wall.rotation, wall);
    }

    void DeleteDoor(GameObject door, int x, int z, DoorPosition position)
    {
        // Update door information on wall data
        WallData wallData = GameDataManager.instance.gameData.wallData;
        switch (position)
        {
            case DoorPosition.top:
                wallData.wallUnits[x, z].Top.wallProp = null;
                break;
            case DoorPosition.right:
                wallData.wallUnits[x, z].Right.wallProp = null;
                break;
            case DoorPosition.bottom:
                wallData.wallUnits[x, z].Bottom.wallProp = null;
                break;
            case DoorPosition.left:
                wallData.wallUnits[x, z].Left.wallProp = null;
                break;
        }
        // Despawn door
        ObjectPooler.instance.DespawnToPool(GameConstants.doorName, door);
    }
}

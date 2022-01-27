using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            if (Input.GetMouseButton(0))
            {
                DoorEditor.instance.AddDoor(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                DoorEditor.instance.AddDoor(Input.mousePosition, true);
            }
            return;
        }
        if (Input.GetButton("Fire1"))
        {
            if (Input.GetMouseButton(0))
            {
                WallEditor.instance.AddWall(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                WallEditor.instance.RemoveWall(Input.mousePosition);
            }
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            FurnitureEditor.instance.PlaceFurniture();
        }
        else if (Input.GetMouseButton(1))
        {
           
        }
    }
}

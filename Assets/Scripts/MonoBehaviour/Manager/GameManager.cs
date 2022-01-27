using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            if (Input.GetMouseButton(0))
            {
                DoorEditor.doorEditor.AddDoor(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                DoorEditor.doorEditor.AddDoor(Input.mousePosition, true);
            }
            return;
        }
        if (Input.GetButton("Fire1"))
        {
            if (Input.GetMouseButton(0))
            {
                WallEditor.wallEditor.AddWall(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                WallEditor.wallEditor.RemoveWall(Input.mousePosition);
            }
            return;
        }

    }
}

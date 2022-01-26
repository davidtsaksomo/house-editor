using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            WallEditor.wallEditor.AddWall(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            WallEditor.wallEditor.RemoveWall(Input.mousePosition);
        }
    }
}

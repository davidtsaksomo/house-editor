using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // Mouse is on top of a GUI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (GameStateManager.instance.gameState == GameState.editingWall)
        {
            if (Input.GetMouseButton(0))
            {
                WallEditor.instance.AddWall(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                WallEditor.instance.RemoveWall(Input.mousePosition);
            }
        }
        else if (GameStateManager.instance.gameState == GameState.editingWallProp)
        {
            if (Input.GetMouseButton(0))
            {
                DoorEditor.instance.AddDoor(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                DoorEditor.instance.AddDoor(Input.mousePosition, true);
            }
        }
        else if (GameStateManager.instance.gameState == GameState.addingFurniture)
        {
            FurnitureEditor.instance.ShowFurnitureOnCursor(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                FurnitureEditor.instance.PlaceFurniture();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (GameStateManager.instance.gameState == GameState.editingFurniture && !Input.GetButton("Fire3"))
        {
            if (!FurnitureEditor.instance.SelectedFurnitureInstanceActive)
            {
                FurnitureEditor.instance.ReloadFurnitureInstance();
            }
            FurnitureEditor.instance.ShowFurnitureOnCursor(Input.mousePosition);
        }
        else if (FurnitureEditor.instance.SelectedFurnitureInstanceActive)
        {
            FurnitureEditor.instance.SelectedFurnitureInstanceActive = false;
        }

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
        else if (GameStateManager.instance.gameState == GameState.editingFurniture)
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetButton("Fire3"))
            {
                FurnitureEditor.instance.PlaceFurniture(Input.mousePosition);
            }
            else if (Input.GetButton("Fire3") && Input.GetMouseButtonDown(1))
            {
                FurnitureEditor.instance.DeleteFurniture(Input.mousePosition);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                FurnitureEditor.instance.CycleFurniture();
            }
            else if ((int)Input.mouseScrollDelta.y != 0)
            {
                FurnitureEditor.instance.RotateFurniture(Input.mouseScrollDelta.y);
            }
        }
    }
}

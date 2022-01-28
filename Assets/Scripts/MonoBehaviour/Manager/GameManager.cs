using UnityEngine;
using UnityEngine.EventSystems;

// Class to receive user input and do action
public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (GameStateManager.instance.gameState == GameState.editingFurniture && !Input.GetKey(KeyCode.LeftShift))
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

        // Mouse not top of a GUI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
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
                    DoorEditor.instance.EditDoor(Input.mousePosition);
                }
                else if (Input.GetMouseButton(1))
                {
                    DoorEditor.instance.EditDoor(Input.mousePosition, true);
                }
            }
            else if (GameStateManager.instance.gameState == GameState.editingFurniture)
            {
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(1))
                {
                    FurnitureEditor.instance.DeleteFurniture(Input.mousePosition);
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        FurnitureEditor.instance.PlaceFurniture(Input.mousePosition);
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
            else if (GameStateManager.instance.gameState == GameState.changingColor)
            {
                if (Input.GetMouseButton(0))
                {
                    ColorChanger.instance.ChangeObjectColor(Input.mousePosition);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ColorChanger.instance.ChangeColorSelection();
                }
            }
        }


        if (Input.GetKey(KeyCode.W))
        {
            CameraManager.instance.MoveUp();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CameraManager.instance.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CameraManager.instance.MoveDown();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CameraManager.instance.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraManager.instance.RotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CameraManager.instance.RotateRight();
        }
    }
}

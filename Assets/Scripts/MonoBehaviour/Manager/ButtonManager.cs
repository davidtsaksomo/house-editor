using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    Button editWallButton = null;
    [SerializeField]
    Button editDoorButton = null;
    [SerializeField]
    Button editFurnitureButton = null;
    [SerializeField]
    Button changeColorButton = null;
    [SerializeField]
    Button saveLayoutButton = null;
    [SerializeField]
    Button loadLayoutButton = null;

    List<Button> stateButtonList = new List<Button>();

    void Start()
    {
        editWallButton.onClick.AddListener( delegate { changeGameState(GameState.editingWall); });
        editDoorButton.onClick.AddListener(delegate { changeGameState(GameState.editingWallProp); });
        editFurnitureButton.onClick.AddListener(delegate { changeGameState(GameState.editingFurniture); });
        changeColorButton.onClick.AddListener(delegate { changeGameState(GameState.changingColor); });

        stateButtonList.Add(editWallButton);
        stateButtonList.Add(editDoorButton);
        stateButtonList.Add(editFurnitureButton);
        stateButtonList.Add(changeColorButton);

        selectButtonBasedOnState(GameStateManager.instance.gameState);
    }

    void changeGameState(GameState gameState)
    {
        GameStateManager.instance.gameState = gameState;
        selectButtonBasedOnState(gameState);
    }

    void selectButtonBasedOnState(GameState gameState)
    {
        Button selectedButton = null;
        switch (gameState)
        {
            case GameState.editingWall:
                selectedButton = editWallButton;
                InformationTextController.instance.setText("Left click and drag to place wall. Right click and drag to remove wall.");
                break;
            case GameState.editingWallProp:
                selectedButton = editDoorButton;
                InformationTextController.instance.setText("Left click on a wall to place door. Right click on a door to remove.");
                break;
            case GameState.editingFurniture:
                selectedButton = editFurnitureButton;
                InformationTextController.instance.setText("Left click to place furniture. Right click change furniture. Scroll wheel to rotate furniture.");
                break;
            case GameState.changingColor:
                selectedButton = changeColorButton;
                InformationTextController.instance.setText("Coming soon.");
                break;
        }
        foreach (Button button in stateButtonList)
        {
            if (button == selectedButton)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}

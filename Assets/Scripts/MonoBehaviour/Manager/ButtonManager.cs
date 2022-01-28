using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to manage button functions
public class ButtonManager : MonoBehaviour
{
    [Tooltip("Button to edit wall.")]
    [SerializeField]
    Button editWallButton = null;
    [Tooltip("Button to edit door.")]
    [SerializeField]
    Button editDoorButton = null;
    [Tooltip("Button to edit furniture.")]
    [SerializeField]
    Button editFurnitureButton = null;
    [Tooltip("Button to change color.")]
    [SerializeField]
    Button changeColorButton = null;
    [Tooltip("Button to save layout.")]
    [SerializeField]
    Button saveLayoutButton = null;
    [Tooltip("Button to load layout.")]
    [SerializeField]
    Button loadLayoutButton = null;

    List<Button> stateButtonList = new List<Button>();

    void Start()
    {
        // Assign button onclick event listener
        editWallButton.onClick.AddListener( () => { changeGameState(GameState.editingWall); });
        editDoorButton.onClick.AddListener( () => { changeGameState(GameState.editingWallProp); });
        editFurnitureButton.onClick.AddListener( () => { changeGameState(GameState.editingFurniture); });
        changeColorButton.onClick.AddListener( () => { changeGameState(GameState.changingColor); });
        saveLayoutButton.onClick.AddListener( () => {
            GameDataManager.instance.SaveLayout();
            InformationTextController.instance.setText("Saved!");
        });
        loadLayoutButton.onClick.AddListener( () => {
            if (GameDataManager.instance.LoadLayout())
            {
                InformationTextController.instance.setText("Loaded!");
            }
            else
            {
                InformationTextController.instance.setText("Can't load");
            }
            
        });

        // add buttons to state buttons list
        stateButtonList.Add(editWallButton);
        stateButtonList.Add(editDoorButton);
        stateButtonList.Add(editFurnitureButton);
        stateButtonList.Add(changeColorButton);

        // Starting game state
        selectButtonBasedOnState(GameStateManager.instance.gameState);
    }

    void changeGameState(GameState gameState)
    {
        GameStateManager.instance.gameState = gameState;
        selectButtonBasedOnState(gameState);
    }

    // Make already selected button not selectable, and display text information according to selected state
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
                InformationTextController.instance.setText("Left click to place furniture. Right click to change furniture. Scroll wheel to rotate furniture. Shift + Right click to delete furniture.");
                break;
            case GameState.changingColor:
                selectedButton = changeColorButton;
                InformationTextController.instance.setText("Left click on wall or furniture to change its color. Right click to change color selection");
                break;
        }

        if (gameState == GameState.changingColor)
        {
            ColorSelectionIndicator.instance.Show();
        }
        else
        {
            ColorSelectionIndicator.instance.Hide();
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

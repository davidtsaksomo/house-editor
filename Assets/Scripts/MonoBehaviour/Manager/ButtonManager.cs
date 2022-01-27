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
    Button addFurnitureButton = null;
    [SerializeField]
    Button editFurnitureButton = null;
    [SerializeField]
    Button changeColorBUtton = null;
    [SerializeField]
    Button saveLayoutButton = null;
    [SerializeField]
    Button loadLayoutButton = null;

    List<Button> stateButtonList = new List<Button>();

    void Start()
    {
        editWallButton.onClick.AddListener( delegate { changeGameState(GameState.editingWall); });
        editDoorButton.onClick.AddListener(delegate { changeGameState(GameState.editingWallProp); });
        addFurnitureButton.onClick.AddListener(delegate { changeGameState(GameState.addingFurniture); });
        editFurnitureButton.onClick.AddListener(delegate { changeGameState(GameState.editingFurniture); });
        changeColorBUtton.onClick.AddListener(delegate { changeGameState(GameState.changingColor); });

        stateButtonList.Add(editWallButton);
        stateButtonList.Add(editDoorButton);
        stateButtonList.Add(addFurnitureButton);
        stateButtonList.Add(editFurnitureButton);
        stateButtonList.Add(changeColorBUtton);

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
                break;
            case GameState.editingWallProp:
                selectedButton = editDoorButton;
                break;
            case GameState.addingFurniture:
                selectedButton = addFurnitureButton;
                break;
            case GameState.editingFurniture:
                selectedButton = editFurnitureButton;
                break;
            case GameState.changingColor:
                selectedButton = changeColorBUtton;
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

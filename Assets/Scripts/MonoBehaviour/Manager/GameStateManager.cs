using UnityEngine;

// State of the game
public enum GameState
{
    editingWall,
    editingWallProp,
    editingFurniture,
    changingColor
}

// Manage the state of the game
public class GameStateManager: MonoBehaviour
{
    // Singleton
    public static GameStateManager instance;

    const GameState startingState = GameState.editingWall;
    [HideInInspector]
    public GameState gameState = startingState;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}
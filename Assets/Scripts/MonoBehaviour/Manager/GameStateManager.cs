using UnityEngine;

public enum GameState
{
    editingWall,
    editingWallProp,
    editingFurniture,
    changingColor
}

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
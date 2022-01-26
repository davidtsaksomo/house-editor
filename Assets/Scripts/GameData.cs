using UnityEngine;

public class GameData : MonoBehaviour
{
    public static WallData wallData = new WallData(GameConfiguration.worldWidth, GameConfiguration.worldLength);
}

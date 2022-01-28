using UnityEngine;

public class GameConstants
{
    // Size of the world
    public const int worldWidth = 90;
    public const int worldLength = 45;

    // The size of the default plane shape from unity
    public const int planeScaleMultiplier = 10;

    public const string wallName = "Wall";
    public const string doorName = "Door";

    public static Color DefaultWallColor
    {
        get => Color.white;
    }

    public static Color DefaultFurnitureColor
    {
        get => Color.white;
    }

}
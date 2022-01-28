// Class to store game data
[System.Serializable]
public class GameData
{
    public WallData wallData = new WallData(GameConstants.worldWidth, GameConstants.worldLength);
    public PlacedFurnitureList placedFurniturelist = new PlacedFurnitureList();
}
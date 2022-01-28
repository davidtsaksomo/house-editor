[System.Serializable]
public class GameData
{
    public WallData wallData = new WallData(GameConstants.worldWidth, GameConstants.worldLength);
    public PlacedFurniturelist placedFurniturelist = new PlacedFurniturelist();
}
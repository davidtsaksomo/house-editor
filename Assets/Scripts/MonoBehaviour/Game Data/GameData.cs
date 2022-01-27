using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Singleton
    public static GameData instance;


    public WallData wallData = new WallData(GameConstants.worldWidth, GameConstants.worldLength);
    public PlacedFurniturelist placedFurniturelist = new PlacedFurniturelist();

    const string wallDataFilePath = "/wallData.dat";
    const string furnitureDataFilePath = "/furnitureData.dat";

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void SaveLayout()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string wallDataPath = Application.persistentDataPath + wallDataFilePath;
        string furnitureDataPath = Application.persistentDataPath + furnitureDataFilePath;

        FileStream stream = new FileStream(wallDataPath, FileMode.Create);
        formatter.Serialize(stream, wallData);
        stream.Close();

        stream = new FileStream(furnitureDataPath, FileMode.Create);
        formatter.Serialize(stream, placedFurniturelist);
        stream.Close();
    }

    public bool LoadLayout()
    {
        string wallDataPath = Application.persistentDataPath + wallDataFilePath;
        string furnitureDataPath = Application.persistentDataPath + furnitureDataFilePath;


        if (!File.Exists(wallDataPath) && !File.Exists(furnitureDataPath))
        {
            return false;
        }

        if (File.Exists(wallDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(wallDataPath, FileMode.Open);
            wallData = formatter.Deserialize(stream) as WallData;
            stream.Close();
        }

        if (File.Exists(furnitureDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(furnitureDataPath, FileMode.Open);
            placedFurniturelist = formatter.Deserialize(stream) as PlacedFurniturelist;
            stream.Close();
        }
        return true;
        
    }
}

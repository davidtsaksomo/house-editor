using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Class to access game data and manage save/load
public class GameDataManager : MonoBehaviour
{
    // Singleton
    public static GameDataManager instance;

    // Store all data of the layout
    public GameData gameData = new GameData();
    
    // Path to save data file
    const string filePath = "/gameData.sav";

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Save current gameData object to file
    public void SaveLayout()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filePath;

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    // Load from file to current game data
    public bool LoadLayout()
    {
        string path = Application.persistentDataPath + filePath;


        if (!File.Exists(path))
        {
            return false;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        gameData = formatter.Deserialize(stream) as GameData;
        stream.Close();

        ApplyDataChanges();
        return true;
    }

    // Adjust the world according to data
    void ApplyDataChanges()
    {
        WallEditor.instance.SpawnWallFromData();
        FurnitureEditor.instance.SpawnFurnitureFromData();
    }
}

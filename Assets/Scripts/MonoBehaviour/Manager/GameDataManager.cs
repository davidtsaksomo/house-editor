using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Singleton
    public static GameDataManager instance;

    public GameData gameData = new GameData();
    

    const string filePath = "/gameData.sav";

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
        string path = Application.persistentDataPath + filePath;

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameData);
        stream.Close();
    }

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

    void ApplyDataChanges()
    {
        WallEditor.instance.SpawnWallFromData();
        FurnitureEditor.instance.SpawnFurnitureFromData();
    }
}

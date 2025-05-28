using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;
    
    private string savePath => Path.Combine(Application.persistentDataPath, "playerData.bin");
    private void Start()
    {
        
        LoadData();
        playerData.saveAction += SaveData;
        
    }
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(playerData, true);
        byte[] binaryFile = JsonToBinary(jsonData);
        File.WriteAllBytes(savePath, binaryFile);
        
    }
     public static byte[] JsonToBinary(string json)
    {
        using (MemoryStream stream = new MemoryStream())
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            // Add header for version control
            writer.Write(1); // Version number
            
            // Write JSON length followed by content
            writer.Write(json.Length);
            writer.Write(json.ToCharArray());
            
            return stream.ToArray();
        }
    }

    public void LoadData()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            return;
        }
        if (File.Exists(savePath))
        {
            byte[] binaryFile = File.ReadAllBytes(savePath);
            string jsonData = BinaryToJson(binaryFile);
            JsonUtility.FromJsonOverwrite(jsonData, playerData);
            Debug.Log("Data loaded");
        }
        else
        {
            Debug.Log("No save file found");
        }
    }
    public static string BinaryToJson(byte[] data)
    {
        using (MemoryStream stream = new MemoryStream(data))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            int version = reader.ReadInt32();
            int length = reader.ReadInt32();
            char[] jsonChars = reader.ReadChars(length);
            return new string(jsonChars);
        }
    }

    void OnDisable()
    {
        playerData.saveAction -= SaveData;
    }
}
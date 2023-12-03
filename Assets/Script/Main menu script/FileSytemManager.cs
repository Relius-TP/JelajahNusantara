using System.IO;
using UnityEngine;

public class FileSytemManager : MonoBehaviour
{
    public static FileSytemManager instance;

    private string savePath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        savePath = Application.persistentDataPath + "/playerSetting.js";
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayerSettings(PlayerSettingsData playerSettingsData)
    {
        string jsonData = JsonUtility.ToJson(playerSettingsData);
        File.WriteAllText(savePath, jsonData);
    }

    public void LoadPlayerSettings()
    {
        if(CheckFile(savePath))
        {
            PlayerSettingsData playerSettingsData = new();
            string jsonData = File.ReadAllText(savePath);
            playerSettingsData = JsonUtility.FromJson<PlayerSettingsData>(jsonData);
        }
        else
        {
            Debug.Log("File tidak ditemukan");
        }
    }

    private bool CheckFile(string savePath)
    {
        if(File.Exists(savePath))
        {
            return true;
        }

        return false;
    }
}

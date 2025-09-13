using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScoreName;
        public int HighScoreValue;
    }

    public void SaveHighScore(int highScore)
    {
        SaveData data = new SaveData();
        data.HighScoreName = playerName;
        data.HighScoreValue = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public Dictionary ReadHighScore()
    {
        Dictionary dict = new Dictionary();

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            dict["name"] = data.HighScoreName;
            dict["score"] = data.HighScoreValue;
        }

        return dict;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public int highScore;

    string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/savefile.json"; 

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (playerName.Length == 0)
        {
            playerName = "Player";
        }

        if (highScore !> 0)
        {
            highScore = 0;
        }

        if (File.Exists(savePath))
        {
            LoadScore();
        }
    }

    [System.Serializable]
    class ScoreInfo
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        ScoreInfo score = new ScoreInfo();
        score.name = playerName;
        score.score = highScore;
        string json = JsonUtility.ToJson(score);
        File.WriteAllText(savePath, json);
    }

    public void UpdateName(string name)
    {
        playerName = name;
    }

    public void LoadScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreInfo data = JsonUtility.FromJson<ScoreInfo>(json);

            highScore = data.score;
            playerName = data.name;
        }
    }

    public void ResetHighScore()
    {
        highScore = 0;
        playerName = "Player";
        SaveScore();
    }
}

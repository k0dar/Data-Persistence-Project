using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    public GameManager Manager;
    public TMP_InputField NameInput;
    public static MainManager Instance;
    public string playerName;
    public int highScore;
    public string savedHighScoreText = "High Score: Not Yet Set";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; 

        DontDestroyOnLoad(gameObject);

        LoadHighScore();

        GameObject.Find("High Score Text").GetComponent<TextMeshProUGUI>().text = savedHighScoreText;
    }

    public void PressedStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void PressedExitButton()
    {
        Application.Quit();
    }

    public void FinishedNameInput()
    {
        playerName = NameInput.text;
    }

    public void NewHighScore()
    {

        GameObject.Find("GameManager").GetComponent<GameManager>().HighScoreText.text = ("High Score: " + playerName + " - " + GameObject.Find("GameManager").GetComponent<GameManager>().m_Points);

        Debug.Log("saved it");
        highScore = GameObject.Find("GameManager").GetComponent<GameManager>().m_Points;
        savedHighScoreText = GameObject.Find("GameManager").GetComponent<GameManager>().HighScoreText.text;

        SaveHighScore();
    }


    [System.Serializable]
    class SaveData
    {
        public string highScoreText;
        public int highScoreScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScoreText = savedHighScoreText;
        data.highScoreScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("Called HighScore Data");

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedHighScoreText = data.highScoreText;
            highScore = data.highScoreScore;

            
        }
    }
}

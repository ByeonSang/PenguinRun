using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    public int CurrentScore { get; set; } = 0;     // 현재점수
    public int BestScore { get; private set; } = 0; // 최대점수

    public bool IsGameOver { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SaveScore(int currentscore)
    {
        if (BestScore <currentscore)
        {
            BestScore = currentscore;
            PlayerPrefs.SetInt("BestScore", BestScore); // (이름, 값)
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        IsGameOver = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        CurrentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void MoveScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

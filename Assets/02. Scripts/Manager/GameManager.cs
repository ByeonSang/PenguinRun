using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    public Character character { get; set; }
    private float time = 0f;
    private float speedDuration = 4f;
    public int CurrentScore { get; set; } = 0;     // 현재점수
    public int BestScore { get; private set; } = 0; // 최대점수

    private float plusSpeed = 0;
    public float PlusSpeed
    {
        get { return plusSpeed; }
        set
        {
            plusSpeed = value;
            time = speedDuration;           
        }
    }

    public bool IsGameOver { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadBestScore();
    }


    private void Update()
    {
        time-= Time.deltaTime;
        if(time <= 0f &&character!=null)
        {
            character.isSpeeding = false;
            plusSpeed = 0;
        }
    }

    private void LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
            BestScore = PlayerPrefs.GetInt("BestScore");
        else
            BestScore = 0;
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
        time = 0;
        CurrentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        time = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void MoveScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

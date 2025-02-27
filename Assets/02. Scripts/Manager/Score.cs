using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI resultBestScoreText;

    public GameObject gameOverUI;

    private bool onResultMenu = false;

    private void Awake()
    {
        UIManager.Instance.updateUI += UpdateUI;
    }

    private void Start()
    {
        Time.timeScale = 1;
        GameManager.Instance.IsGameOver = false;
        onResultMenu = false;
        gameOverUI.SetActive(false);
    }
    /// <summary>
    /// 점수를 올리는 메서드
    /// </summary>
    /// <param name="num">증가시킬 점수</param>

    private void Update()
    {
        if (GameManager.Instance.IsGameOver && onResultMenu ==false)
        {
            SaveScore();
            UpdateUI();
            gameOverUI.SetActive(true);
            onResultMenu = true;
        }
    }

    public void UpdateUI()
    {
        GameManager gameManager = GameManager.Instance;
        scoreText.text = gameManager.CurrentScore.ToString();
        resultScoreText.text = scoreText.text;

        bestScoreText.text = gameManager.BestScore.ToString();
        resultBestScoreText.text = bestScoreText.text;
    }

    public void SaveScore()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.SaveScore(gameManager.CurrentScore);
    }


}

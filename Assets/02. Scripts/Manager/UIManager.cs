using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI resultBestScoreText;
    public GameObject gameOverUI;
    // public Animator animator;
    // public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
        // scoreManager = GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void GameOver()
    {
        Score.Instance.SaveScore();
        gameOverUI.SetActive(true);
    }

    public void UpdateUI()
    {
        scoreText.text = Score.Instance.CurrentScore.ToString();
        resultScoreText.text = scoreText.text;
        
        bestScoreText.text = Score.Instance.BestScore.ToString();
        resultBestScoreText.text = bestScoreText.text;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        SceneManager.LoadScene("StartScene");
    }
}

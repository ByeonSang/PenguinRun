using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    static private UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager)GameObject.FindObjectOfType(typeof(UIManager));
                if (Instance == null)
                {
                    GameObject obj = new GameObject(typeof(UIManager).Name, typeof(UIManager));
                    instance = obj.GetComponent<UIManager>();
                    DontDestroyOnLoad(obj);
                }
            }

            return instance;
        }
    }


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI resultBestScoreText;
    public GameObject gameOverUI;
    // public Animator animator;
    // public ScoreManager scoreManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    

    public void MoveScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }

    public void GameOver()
    {
        ScoreManager.Instance.SaveScore();
        gameOverUI.SetActive(true);
    }

    public void UpdateUI()
    {
        scoreText.text = ScoreManager.Instance.Score.ToString();
        resultScoreText.text = scoreText.text;
        
        bestScoreText.text = ScoreManager.Instance.BestScore.ToString();
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

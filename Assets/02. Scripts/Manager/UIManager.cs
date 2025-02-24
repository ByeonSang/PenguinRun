using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//점수 반영
//게임 오버창 띄우기
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
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
        if (Input.GetMouseButtonDown(0))
        {
            scoreText.text = ScoreManager.Instance.Score.ToString();
            gameOverUI.SetActive(true); // 오브젝트 활성화 (bool값)
        }

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        scoreText.gameObject.SetActive(true);

        ScoreManager.Instance.AddScore(1);
    }
}

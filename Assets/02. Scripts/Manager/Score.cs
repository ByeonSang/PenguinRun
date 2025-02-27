using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI resultBestScoreText;

    public GameObject questSuccessPanel;
    public AnimationCurve animCurve;
    public float moveDuration;

    public GameObject gameOverUI;

    private bool onResultMenu = false;

    private void Awake()
    {
        UIManager.Instance.updateUI += UpdateUI;
        QuestManager.Instance.successQuest += UpQuestSuccessPanel;
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

    public IEnumerator UpQuestSuccessPanel()
    {
        float currentTime = 0;
        float percent = 0;
        RectTransform rectTransform = questSuccessPanel.GetComponent<RectTransform>();
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveDuration;
            float curveTime = animCurve.Evaluate(percent);
            Vector2 nowPos = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Vector2.Lerp(nowPos, Vector2.zero, curveTime);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        currentTime = 0;
        percent = 0;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveDuration;
            float curveTime = animCurve.Evaluate(percent);
            Vector2 nowPos = rectTransform.anchoredPosition;
            Vector2 destination = new Vector2(nowPos.x, -100);
            rectTransform.anchoredPosition = Vector2.Lerp(nowPos, destination, curveTime);
            yield return null;
        }
    }


}

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


    // UI 업데이트 이벤트에 UpdateUI() 등록
    private void Awake()
    {
        UIManager.Instance.updateUI += UpdateUI;
        QuestManager.Instance.successQuest += UpQuestSuccessPanel;
    }


    // 게임 시작 시 초기화
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


    // 게임 오버 상태를 감지하고 결과 메뉴를 한 번만 실행하도록 설정
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

    // 현재 점수와 최고 점수를 UI에 반영하는 함수
    public void UpdateUI()
    {
        GameManager gameManager = GameManager.Instance;
        scoreText.text = gameManager.CurrentScore.ToString();
        resultScoreText.text = scoreText.text;

        bestScoreText.text = gameManager.BestScore.ToString();
        resultBestScoreText.text = bestScoreText.text;
    }

    // 최고 점수를 저장하는 함수
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

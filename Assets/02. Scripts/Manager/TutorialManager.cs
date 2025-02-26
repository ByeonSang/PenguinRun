using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI coachingText;
    private bool isTutorialActive = true; // 튜토리얼 진행 중 여부
    public GameObject tutorialPanel; // 텍스트 박스 UI 오브젝트

    public GameObject HpList;
    string[] jumpStr = { "안녕! 펭귄런에 온 걸 환영해! ", "간단한 펭귄런 조작법을 배워볼거야", "스페이스바를 눌러, 점프키를 배워보자" };
    string[] slideStr = { "이제 슬라이딩을 배워볼거야", "왼쪽 쉬프트키를 눌러서 슬라이딩을 해보자" };
    string[] twoJumpStr = { "이제 2단점프를 배워볼까?", "스페이스바를 두 번 눌러서 2단점프를 해보자!" };
    string[] itemStr = { "빨간색 눈송이는 Hp를 회복해!", "파란색 눈송이는 속도가 올라가고\n 장애물을 파괴하지!" };

    private bool firstJump = false;
    private bool secondJump = false;
   
    void Start()
    {
     
        StartCoroutine(WriteText());
    }


    IEnumerator WriteText()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f; // 게임 멈추기
        foreach (string message in jumpStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // 게임 멈춰도 시간 흐르게
        }

        tutorialPanel.SetActive(false); // 동작동안 판넬 제거
        Time.timeScale = 1f; // 게임 동작
        yield return StartCoroutine(TJump()); // 점프 실행
        PerfectText();
        yield return new WaitForSecondsRealtime(2f); // 텍스트 확인할때 까지 2초 기다리기

        // 슬라이드 배워보기
        Time.timeScale = 0f; // 게임 멈추기

        foreach (string message in slideStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // 게임 멈춰도 시간 흐르게
        }
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f; // 게임 시작

        yield return StartCoroutine(TSlide());   // 슬라이드
        PerfectText();
        yield return new WaitForSecondsRealtime(2f); // 텍스트 확인할때 까지 2초 기다리기

        // 이단점프 배워보기
        Time.timeScale = 0f; // 게임 멈추기
        foreach (string message in twoJumpStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // 게임 멈춰도 시간 흐르게
        }
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        yield return StartCoroutine(SecondJump());
        if (secondJump)
            PerfectText();
        yield return new WaitForSecondsRealtime(2f);

        // 아이템 사용해보기

        Time.timeScale = 0f;
        foreach (string message in itemStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f);
        }
        tutorialPanel.SetActive(false);
       
        Time.timeScale = 1f;

        tutorialPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        coachingText.text = "수고했어 이제 시작화면으로 돌아가서 진짜 게임을 해봐!";
        yield return new WaitForSecondsRealtime(5f);

        SceneManager.LoadScene("StartScene");

    }

    IEnumerator TJump()
    {

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        tutorialPanel.SetActive(true);
    }

    IEnumerator TSlide()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
        tutorialPanel.SetActive(true);
    }


    IEnumerator SecondJump()
    {
        firstJump = false;
        secondJump = false;

        // 첫 번째 점프 감지
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        firstJump = true; // 첫 번째 점프 완료
        coachingText.text = "한 번 더!";
        tutorialPanel.SetActive(true);

        // 일정 시간 내에 두 번째 점프 감지 (0.5초 제한)
        float timer = 0f;
        float maxTime = 2f; // 두 번째 점프를 받을 최대 시간
        while (timer < maxTime)
        {
            timer += Time.unscaledDeltaTime; // Time.timeScale = 0f 상태에서도 동작
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                secondJump = true;
                tutorialPanel.SetActive(true);
                PerfectText();
                yield break; // 두 번째 점프가 감지되면 종료
            }
        }

        // 제한 시간 초과 시 초기화
        firstJump = false;
    }

    string PerfectText()
    {
        return coachingText.text = "잘했어!";
    }

  

    public void ChangeScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

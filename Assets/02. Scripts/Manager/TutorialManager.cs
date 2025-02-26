using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using System;
public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI coachingText;
    private bool isTutorialActive = true; // 튜토리얼 진행 중 여부
    public GameObject tutorialPanel; // 텍스트 박스 UI 오브젝트

    string[] jumpStr = { "안녕! 펭귄런에 온 걸 환영해! ", "간단한 펭귄런 조작법을 배워볼거야", "스페이스바를 눌러, 점프키를 배워보자" };
    string[] slideStr = { "이제 슬라이딩을 배워볼거야", "왼쪽 쉬프트키를 눌러서 슬라이딩을 해보자" };
    bool isTextBox = false;
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
        coachingText.text = "잘했어";

        yield return new WaitForSecondsRealtime(2f); // 텍스트 확인할때 까지 2초 기다리기
        Time.timeScale = 0f; // 게임 멈추기

    }

    IEnumerator TJump()
    {

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        tutorialPanel.SetActive(true);
    }


  
}

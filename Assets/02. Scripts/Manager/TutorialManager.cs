using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using System;
using UnityEditor.VersionControl;
public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI coachingText;
    private bool isTutorialActive = true; // 튜토리얼 진행 중 여부
    public GameObject tutorialPanel; // 텍스트 박스 UI 오브젝트

    string[] str = { "안녕! 펭귄런에 온 걸 환영해! ", "간단한 펭귄런 조작법을 배워볼거야" ,"스페이스바를 눌러, 점프키를 배워보자"};
    string[] slideStr = { "you can slide", "Press shift" };
    bool isTextBox = false;
    void Start()
    {
        StartCoroutine(WriteText());
    }

    void Update()
    {
      
    }
    IEnumerator WriteText()
    {
        Time.timeScale = 0f; // 게임 멈추기

        foreach (string message in str)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // 게임 멈춰도 시간 흐르게
        }

        Time.timeScale = 1f;
        
        TJump();

        foreach (string message in slideStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // 게임 멈춰도 시간 흐르게
        }

        TSlide();
    }
 
     IEnumerator TJump()
    {
        tutorialPanel.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // 스페이스바 입력 대기

        coachingText.text = "Perfect!";
        tutorialPanel.SetActive(true);

    }

    public void TSlide()
    {

       
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
           
            coachingText.text = "Perfect!";

        }
    }
}

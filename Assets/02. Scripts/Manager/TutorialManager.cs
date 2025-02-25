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
    private bool isTutorialActive = true; // Ʃ�丮�� ���� �� ����
    public GameObject tutorialPanel; // �ؽ�Ʈ �ڽ� UI ������Ʈ

    string[] str = { "�ȳ�! ��Ϸ��� �� �� ȯ����! ", "������ ��Ϸ� ���۹��� ������ž�" ,"�����̽��ٸ� ����, ����Ű�� �������"};
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
        Time.timeScale = 0f; // ���� ���߱�

        foreach (string message in str)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // ���� ���絵 �ð� �帣��
        }

        Time.timeScale = 1f;
        
        TJump();

        foreach (string message in slideStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // ���� ���絵 �ð� �帣��
        }

        TSlide();
    }
 
     IEnumerator TJump()
    {
        tutorialPanel.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // �����̽��� �Է� ���

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

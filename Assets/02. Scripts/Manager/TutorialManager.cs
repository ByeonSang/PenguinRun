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
    private bool isTutorialActive = true; // Ʃ�丮�� ���� �� ����
    public GameObject tutorialPanel; // �ؽ�Ʈ �ڽ� UI ������Ʈ

    string[] jumpStr = { "�ȳ�! ��Ϸ��� �� �� ȯ����! ", "������ ��Ϸ� ���۹��� ������ž�", "�����̽��ٸ� ����, ����Ű�� �������" };
    string[] slideStr = { "���� �����̵��� ������ž�", "���� ����ƮŰ�� ������ �����̵��� �غ���" };
    bool isTextBox = false;
    void Start()
    {
        StartCoroutine(WriteText());
    }


    IEnumerator WriteText()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f; // ���� ���߱�
        foreach (string message in jumpStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // ���� ���絵 �ð� �帣��
        }

        tutorialPanel.SetActive(false); // ���۵��� �ǳ� ����
        Time.timeScale = 1f; // ���� ����
        yield return StartCoroutine(TJump()); // ���� ����
        coachingText.text = "���߾�";

        yield return new WaitForSecondsRealtime(2f); // �ؽ�Ʈ Ȯ���Ҷ� ���� 2�� ��ٸ���
        Time.timeScale = 0f; // ���� ���߱�

    }

    IEnumerator TJump()
    {

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        tutorialPanel.SetActive(true);
    }


  
}

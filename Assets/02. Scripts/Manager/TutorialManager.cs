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
    private bool isTutorialActive = true; // Ʃ�丮�� ���� �� ����
    public GameObject tutorialPanel; // �ؽ�Ʈ �ڽ� UI ������Ʈ

    public GameObject HpList;
    string[] jumpStr = { "�ȳ�! ��Ϸ��� �� �� ȯ����! ", "������ ��Ϸ� ���۹��� ������ž�", "�����̽��ٸ� ����, ����Ű�� �������" };
    string[] slideStr = { "���� �����̵��� ������ž�", "���� ����ƮŰ�� ������ �����̵��� �غ���" };
    string[] twoJumpStr = { "���� 2�������� �������?", "�����̽��ٸ� �� �� ������ 2�������� �غ���!" };
    string[] itemStr = { "������ �����̴� Hp�� ȸ����!", "�Ķ��� �����̴� �ӵ��� �ö󰡰�\n ��ֹ��� �ı�����!" };

    private bool firstJump = false;
    private bool secondJump = false;
   
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
        PerfectText();
        yield return new WaitForSecondsRealtime(2f); // �ؽ�Ʈ Ȯ���Ҷ� ���� 2�� ��ٸ���

        // �����̵� �������
        Time.timeScale = 0f; // ���� ���߱�

        foreach (string message in slideStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // ���� ���絵 �ð� �帣��
        }
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f; // ���� ����

        yield return StartCoroutine(TSlide());   // �����̵�
        PerfectText();
        yield return new WaitForSecondsRealtime(2f); // �ؽ�Ʈ Ȯ���Ҷ� ���� 2�� ��ٸ���

        // �̴����� �������
        Time.timeScale = 0f; // ���� ���߱�
        foreach (string message in twoJumpStr)
        {
            coachingText.text = message;
            yield return new WaitForSecondsRealtime(2f); // ���� ���絵 �ð� �帣��
        }
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        yield return StartCoroutine(SecondJump());
        if (secondJump)
            PerfectText();
        yield return new WaitForSecondsRealtime(2f);

        // ������ ����غ���

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
        coachingText.text = "�����߾� ���� ����ȭ������ ���ư��� ��¥ ������ �غ�!";
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

        // ù ��° ���� ����
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        firstJump = true; // ù ��° ���� �Ϸ�
        coachingText.text = "�� �� ��!";
        tutorialPanel.SetActive(true);

        // ���� �ð� ���� �� ��° ���� ���� (0.5�� ����)
        float timer = 0f;
        float maxTime = 2f; // �� ��° ������ ���� �ִ� �ð�
        while (timer < maxTime)
        {
            timer += Time.unscaledDeltaTime; // Time.timeScale = 0f ���¿����� ����
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                secondJump = true;
                tutorialPanel.SetActive(true);
                PerfectText();
                yield break; // �� ��° ������ �����Ǹ� ����
            }
        }

        // ���� �ð� �ʰ� �� �ʱ�ȭ
        firstJump = false;
    }

    string PerfectText()
    {
        return coachingText.text = "���߾�!";
    }

  

    public void ChangeScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

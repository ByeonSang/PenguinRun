using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance { get { return instance; } }

    public int currentCombo = 0;
    public int questSuccessCombo = 3;
    public bool isSuccess = false;

    public Func<IEnumerator> successQuest;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StopAllCoroutines();
    }

    //1�� ����Ʈ ����-----------------------------------------------
    public void AddComboCount() // ��ֹ��� ����� ���� �ʰ� �Ѿ�� �� �� �޼��带 ȣ��
    {
        if(currentCombo < questSuccessCombo)
        {
            currentCombo += 1;
            CheckComboCount();
        }
    }
    private void CheckComboCount()
    {
        if (currentCombo >= questSuccessCombo)
        {
            SuccessQuest1();
        }
    }
    private void SuccessQuest1()
    {
        isSuccess = true;
        StartCoroutine(successQuest());
        //�������� �����ߴٴ� UI ������ �ؿ� ����
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //2�� ����Ʈ ����-----------------------------------------------

}

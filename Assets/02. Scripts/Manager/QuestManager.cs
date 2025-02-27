using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    //1번 퀘스트 관련-----------------------------------------------
    public void AddComboCount() // 장애물을 대미지 받지 않고 넘어갔을 때 이 메서드를 호출
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
        //도전과제 성공했다는 UI 오른쪽 밑에 띄우기
    }





    //2번 퀘스트 관련-----------------------------------------------

}

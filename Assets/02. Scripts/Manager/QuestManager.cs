using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance { get { return instance; } }

    public int currentCombo = 0;
    public int questSuccessCombo = 10;

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
        currentCombo += 1;
        CheckComboCount();
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
        //도전과제 성공했다는 UI 오른쪽 밑에 띄우기
        //시작 창에서 도전과제 확인할 때 성공한 것으로 나오게 하기
    }





    //2번 퀘스트 관련-----------------------------------------------

}

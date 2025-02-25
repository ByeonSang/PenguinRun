using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // 단 하나 존재 (고정), 프리펩에도 사용 가능
    //프리펩의 컴포넌트 필드에는 씬 안에 있는 스크립트를 등록 할 수 없어서 등록할 필요가 없는 싱글톤은 사용 가능함.
    //기본적으로는 스크립트 등록을 하고 그 이름으로 매서드를 사용함.

    public int Score { get; private set; } = 0;     // 현재점수
    public int BestScore { get; private set; } = 0; // 최대점수

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;

            // 이 오브젝트가 씬이 전환되어도 유지되도록 설정 (필요한 경우)
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("ScoreManager 인스턴스가 중복되어 제거됩니다!", gameObject);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 점수를 올리는 메서드
    /// </summary>
    /// <param name="num">증가시킬 점수</param>
    public void AddScore(int num)
    {
        Score += num;
    }

    public void SaveScore()
    {
        if (BestScore < Score)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore); // (이름, 값)
        }
    }
}

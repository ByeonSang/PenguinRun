using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Action updateUI;



    static private UIManager instance;
    private static bool _ShuttingDown = false; // 종료했을때 체크

    public static UIManager Instance
    {
        get
        {
            if(_ShuttingDown)
            {
                Debug.Log($"[SingleTon] Instance {typeof(UIManager)} already destroyed. Returning null");
                return null;
            }

            if (instance == null)
            {
                instance = (UIManager)GameObject.FindObjectOfType(typeof(UIManager));
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(UIManager).Name, typeof(UIManager));
                    instance = obj.GetComponent<UIManager>();
                    DontDestroyOnLoad(obj);
                }
            }

            return instance;
        }
    }

   
    // public Animator animator;
    // public ScoreManager scoreManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }

    private void OnDestroy()
    {
        _ShuttingDown = true;

    }

}

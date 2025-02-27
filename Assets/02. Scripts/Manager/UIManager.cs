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
    public static UIManager Instance
    {
        get
        {
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
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSceneButton : MonoBehaviour
{
    [SerializeField] private int seceneIndex;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        UIManager.Instance.MoveScene(seceneIndex);
    }
}

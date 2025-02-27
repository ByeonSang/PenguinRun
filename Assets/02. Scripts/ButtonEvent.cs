using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum BtnEvent
{
    Restart,
    Title
}
public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private BtnEvent btnEvent;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ClickEvent);
    }

    private void ClickEvent()
    {
        switch (btnEvent)
        {
            case BtnEvent.Restart:
                GameManager.Instance.Restart();
                break;
            case BtnEvent.Title:
                GameManager.Instance.Title();
                break;
        }


    }
}

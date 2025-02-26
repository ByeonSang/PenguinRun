using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalettleColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spr;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskButton);
    }
    private void TaskButton()
    {
        Color color = GetComponent<Image>().color;
        spr.color = color;

        string colorString = string.Format("{0}/{1}/{2}/{3}", color.r, color.g, color.b, color.a);
        PlayerPrefs.SetString("PlayerColor", colorString);
        PlayerPrefs.SetInt("HasColor", 1); // 선택된 컬러가 있으면 컬러선택 씬 넘어가기
    }
}

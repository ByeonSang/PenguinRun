using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private GameObject settingMenu;
    public void Active()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
    }
}

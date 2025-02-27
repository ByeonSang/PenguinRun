using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : BaseItem
{
    [SerializeField] private int value;
    public override void Init()
    {
        base.Init();
        ItemID = 0;
    }
    public override void Use()
    {
        base.Use();
        GameManager.Instance.CurrentScore  += value;
        UIManager.Instance.updateUI();
        audioManager.PlaySFX("Eating02");
    }
}

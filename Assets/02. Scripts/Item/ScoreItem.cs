using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : BaseItem
{
    [SerializeField] private int value;
    private Score scoreManager;
    public override void Init()
    {
        base.Init();
        scoreManager = FindObjectOfType<Score>();
        ItemID = 0;
    }
    public override void Use()
    {
        base.Use();
        //scoreManager.AddScore(value);
        audioManager.PlaySFX("Eating01");
    }
}

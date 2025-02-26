using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : BaseItem
{
    [SerializeField] private int value;
    private ScoreManager scoreManager;
    public override void Init()
    {
        base.Init();
        scoreManager = FindObjectOfType<ScoreManager>();
        ItemID = 0;
    }
    public override void Use()
    {
        base.Use();
        //scoreManager.AddScore(value);
        audioManager.PlaySFX("Eating01");
    }
}

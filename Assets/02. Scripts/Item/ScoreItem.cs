using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : BaseItem
{
    [SerializeField] private int value;

    public override void Use()
    {
        base.Use();
        //player.score += value;      
    }
}

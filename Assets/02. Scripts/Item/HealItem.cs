using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseItem
{
    [SerializeField] private int value;

    public override void Use()
    {
        base.Use();
        //player.HP += value;
    }
}

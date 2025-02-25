using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseItem
{
    [SerializeField] private int value;
    //private Player player;
    public override void Init()
    {
        base.Init();

        ItemID = 1;
        //player = FindObjectOfType<Player>;
    }
    public override void Use()
    {
        base.Use();
        //player.HP += value;
    }
}

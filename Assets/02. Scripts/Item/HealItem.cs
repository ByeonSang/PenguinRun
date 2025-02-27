using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : BaseItem
{
    [SerializeField] private float value = 100;
    public override void Init()
    {
        base.Init();

        ItemID = 1;
    }
    public override void Use()
    {
        base.Use();
        character.Heal(value);
        audioManager.PlaySFX("Eating01");
    }
}

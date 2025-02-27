using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{

    public override void Init()
    {
        base.Init();
        ItemID = 2;
    }
    public override void Use()
    {
        base.Use();
        GameManager.Instance.character.isSpeeding = true;
        GameManager.Instance.PlusSpeed = 2f;
        audioManager.PlaySFX("Eating01");
    }


}

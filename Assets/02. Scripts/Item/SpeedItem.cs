using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    [SerializeField] private float speed;

    public override void Use()
    {
        base.Use();
        StartCoroutine(SpeedingCo(speed));//스피드 값은 언제든 변경 가능
    }

    private IEnumerator SpeedingCo(float speed)
    {
        //player.IsSpeeding = true;
        //player.Speed += speed;
        yield return new WaitForSeconds(4);
        //player.IsSpeeding = false;
        //player.Speed -= speed;
        Destroy(this);
    }
}

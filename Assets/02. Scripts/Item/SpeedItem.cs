using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    [SerializeField] private float speed;

    public override void Use()
    {
        base.Use();
        StartCoroutine(SpeedingCo(speed));//���ǵ� ���� ������ ���� ����
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

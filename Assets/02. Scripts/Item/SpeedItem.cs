using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    [SerializeField] private float speed;
    private Level level;
    //private Player player;
    public static Coroutine speedCo = null;

    public override void Init()
    {
        level = FindObjectOfType<Level>();
        base.Init();
        ItemID = 3;
    }
    public override void Use()
    {
        base.Use();
        if (speedCo == null)
        {
            speedCo = StartCoroutine(SpeedingCo(speed));//스피드 값은 언제든 변경 가능
        }
        else if(speedCo != null)
        {
            StopAllCoroutines();
            EndBuffEffect();
            speedCo = StartCoroutine(SpeedingCo(speed));
        }
    }

    private IEnumerator SpeedingCo(float speed)
    {
        //player.IsSpeeding = true;
        level.bgSpeed += speed;
        yield return new WaitForSeconds(4);
        EndBuffEffect();
    }

    private void EndBuffEffect()
    {
        //player.IsSpeeding = false;
        level.bgSpeed -= speed;
        speedCo = null;
        Destroy(this.gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}

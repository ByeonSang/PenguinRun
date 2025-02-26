using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    [SerializeField] private float speed;
    private Level[] levels;
    public static Coroutine speedCo = null;

    public override void Init()
    {
        base.Init();
        levels = FindObjectsOfType<Level>();
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
        audioManager.PlaySFX("Eating02");
    }

    private IEnumerator SpeedingCo(float speed)
    {
        character.isSpeeding = true; //플레이어 장애물 파괴 모드     
        foreach (Level level in levels) 
        {
            level.bgSpeed += speed;
        }
        yield return new WaitForSeconds(4);
        EndBuffEffect();
    }

    private void EndBuffEffect()
    {
        character.isSpeeding = false;
        foreach (Level level in levels)
        {
            level.bgSpeed -= speed;
        }
        speedCo = null;
        Destroy(this.gameObject);
    }
}

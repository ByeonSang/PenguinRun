using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    [SerializeField] private float speed = 3;
    private Level[] levels;
    public static Coroutine speedCo = null;
    public SpriteRenderer speedItemRenderer;

    public override void Init()
    {
        base.Init();
        levels = FindObjectsOfType<Level>();
        speedItemRenderer = GetComponentInChildren<SpriteRenderer>();
        ItemID = 2;
    }
    public override void Use()
    {
        base.Use();
        speedItemRenderer.gameObject.SetActive(false);
        if (speedCo == null)
        {
            speedCo = StartCoroutine(SpeedingCo(speed));//���ǵ� ���� ������ ���� ����
        }
        else
        {
            StopAllCoroutines();
            EndBuffEffect();
            speedCo = StartCoroutine(SpeedingCo(speed));
        }
        audioManager.PlaySFX("Eating01");
    }

    private IEnumerator SpeedingCo(float speed)
    {
        character.isSpeeding = true; //�÷��̾� ��ֹ� �ı� ���     
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

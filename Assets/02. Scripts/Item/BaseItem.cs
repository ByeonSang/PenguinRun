using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
    void Use();
}
public class BaseItem : MonoBehaviour, IUseable
{
    public int ItemID;
    protected Character character;
    protected AudioManager audioManager = AudioManager.Instance;
    protected CircleCollider2D circleCollider;

    private void Awake()
    {
        Init();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public virtual void Use() // 사용하는 메서드가 없으면 이건 abstract로 넘겨주세요
    {
        circleCollider.enabled = false;
    }

    public  virtual void Init()//Instantiate해준 후 실행
    {
        character = FindObjectOfType<Character>();
    }
}

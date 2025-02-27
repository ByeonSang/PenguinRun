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

    public virtual void Use() // ����ϴ� �޼��尡 ������ �̰� abstract�� �Ѱ��ּ���
    {
        circleCollider.enabled = false;
    }

    public  virtual void Init()//Instantiate���� �� ����
    {
        character = FindObjectOfType<Character>();
    }
}

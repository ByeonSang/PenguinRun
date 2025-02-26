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

    private void Awake()
    {
        Init();
    }

    public virtual void Use() // ����ϴ� �޼��尡 ������ �̰� abstract�� �Ѱ��ּ���
    {

    }

    public  virtual void Init()//Instantiate���� �� ����
    {
        character = FindObjectOfType<Character>();
    }
}

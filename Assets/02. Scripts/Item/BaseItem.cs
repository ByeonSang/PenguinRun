using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
    void Use();
}
public class BaseItem : MonoBehaviour, IUseable
{
    protected int ItemID;
    //protected Player player;
    public virtual void Use()
    {

    }

    public  virtual void Init()//Instantiate���� �� ����
    {
        //player = FindObjectOfType<Player>;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) // �÷��̾����� �� ���� �Ѱ� �ֱ�
    {
        if (LayerMask.NameToLayer(collision.gameObject.name).Equals(LayerMask.NameToLayer("Player")))
        {
            //player = collision.gameObject.GetComponent<Player>();
            Use();
            Destroy(this);//�Ѱ����� �� collision.gameObject�� ����
        }
    }
}

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

    public  virtual void Init()//Instantiate해준 후 실행
    {
        //player = FindObjectOfType<Player>;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) // 플레이어한테 이 로직 넘겨 주기
    {
        if (LayerMask.NameToLayer(collision.gameObject.name).Equals(LayerMask.NameToLayer("Player")))
        {
            //player = collision.gameObject.GetComponent<Player>();
            Use();
            Destroy(this);//넘겨줬을 때 collision.gameObject로 변경
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
    void Use();
}
public class BaseItem : MonoBehaviour, IUseable
{
    //protected Player player;
    public virtual void Use()
    {

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.NameToLayer(collision.gameObject.name).Equals(LayerMask.NameToLayer("Player")))
        {
            //player = collision.gameObject.GetComponent<Player>();
            Use();
            Destroy(this);
        }
    }
}

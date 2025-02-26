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
    protected Character character;
    protected AudioManager audioManager;
    public virtual void Use()
    {

    }

    public  virtual void Init()//Instantiate���� �� ����
    {
        character = FindObjectOfType<Character>();
        audioManager = GetComponent<AudioManager>();
    }
}

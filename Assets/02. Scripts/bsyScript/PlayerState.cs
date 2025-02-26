using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    protected float time;

    protected Player player;
    protected StateMachine stateMachine;
    protected Animator animator;
    protected string boolName;

    public PlayerState(Player _player,StateMachine _stateMachine, Animator _animator, string _boolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animator = _animator;
        boolName = _boolName;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {
        time -= Time.deltaTime;
    }

    public virtual void Exit()
    {

    }
}

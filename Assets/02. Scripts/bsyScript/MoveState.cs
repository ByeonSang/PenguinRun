using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    public MoveState(Player _player, StateMachine _stateMachine, Animator _animator, string _boolName) : base(_player, _stateMachine, _animator, _boolName)
    {
    }

    public override void Enter()
    {
        animator.SetBool(boolName, true);
    }

    public override void Exit()
    {
        animator.SetBool(boolName, false);
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && player.GroundDetection())
        {
            stateMachine.ChanageState(player.airState);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && player.GroundDetection())
        {
            stateMachine.ChanageState(player.slideState);
        }
    }
}

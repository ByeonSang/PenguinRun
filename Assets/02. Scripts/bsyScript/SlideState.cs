using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerState
{
    public SlideState(Player _player, StateMachine _stateMachine, Animator _animator, string _boolName) : base(_player, _stateMachine, _animator, _boolName)
    {
    }

    public override void Enter()
    {
        player.Slide();
        animator.SetBool(boolName, true);
    }

    public override void Exit()
    {
        player.SlideExit();
        animator.SetBool(boolName, false);
    }

    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
            stateMachine.ChanageState(player.moveState);
    }
}

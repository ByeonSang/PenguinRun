using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : PlayerState
{
    float duration = 0.1f;

    public LandingState(Player _player, StateMachine _stateMachine, Animator _animator, string _animationName) : base(_player,_stateMachine, _animator, _animationName)
    {
    }

    public override void Enter()
    {
        time = duration;
        animator.SetBool("IsGround", true);
    }

    public override void Exit()
    {
        animator.SetBool("IsGround", false);
    }

    public override void Update()
    {
        base.Update();

        if (time <= 0)
            stateMachine.ChanageState(player.moveState);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirState : PlayerState
{
    private bool canTowJump;
    public AirState(Player _player, StateMachine _stateMachine, Animator _animator, string _boolName) : base(_player, _stateMachine, _animator, _boolName)
    {
    }

    public override void Enter()
    {
        animator.SetBool(boolName, true);
        canTowJump = true;
        player.Jump();
    }

    public override void Exit()
    {
        animator.SetBool(boolName, false);
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canTowJump)
        {
            canTowJump = false;
            player.TwoJump();
            animator.SetTrigger("TwoJump");
        }

        animator.SetFloat("VerticalVelocity", player.Velocity.y);

        if (player.Velocity.y == 0 && player.GroundDetection())
            stateMachine.ChanageState(player.landingState);
    }
}

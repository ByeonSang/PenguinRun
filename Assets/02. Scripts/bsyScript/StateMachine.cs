using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Player player;

    public StateMachine(Player _player)
    {
        player = _player;
    }

    public void Initialize()
    {
        player.currentState = player.moveState;
        player.currentState.Enter();
    }

    public void ChanageState(PlayerState nextState)
    {
        if (player.currentState != null)
            player.currentState.Exit();

        player.currentState = nextState;
        nextState.Enter();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleBehavior : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        base.PlayerInit();
        player.SetIsInput(true);
        player._isDead = false;
        hostRigidbody2D.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (player.inputX != 0 && isOnGoround)
        {
            hostStateMachine.ChangeState<PlayerRunBehavior>(RoleAnimator.BehaviorNameAndNumToString(BehaviorContainer.RoleBehavior.Run));
        }
        else if (!player.isOnGround)
        {
            hostStateMachine.ChangeState<PlayerFallBehavior>("Fall1");
        }


    }
}

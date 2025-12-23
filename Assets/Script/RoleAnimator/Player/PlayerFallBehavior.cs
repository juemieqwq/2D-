using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallBehavior : RoleBaseState
{
    public override void Enter()
    {
        base.Enter();
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
        hostRigidbody2D.velocity = new Vector2(hostInfo.GetInfo(GetInfoType.Speed) * .5f * (host as Player).inputX, hostRigidbody2D.velocity.y);
        if ((host as Player).isOnGround)
        {
            hostStateMachine.ChangeState<PlayerIdleBehavior>("Idle1");
        }
    }
}

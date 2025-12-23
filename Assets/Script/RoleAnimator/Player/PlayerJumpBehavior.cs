using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBehavior : RoleBaseState
{
    public override void Enter()
    {
        isLoop = false;
        base.Enter();
        hostRigidbody2D.velocity = new Vector2(hostRigidbody2D.velocity.x, hostInfo.GetInfo(GetInfoType.ForceJump));
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

        if (hostRigidbody2D.velocity.y <= 0)
            hostStateMachine.ChangeState<PlayerFallBehavior>("Fall1");
    }
}

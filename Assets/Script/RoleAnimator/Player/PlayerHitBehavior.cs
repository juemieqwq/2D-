using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitBehavior : PlayerBaseState
{
    float time;
    public override void Enter()
    {
        isLoop = false;
        PlayerInit();
        base.Enter();
        hostRigidbody2D.velocity = new Vector2(0, hostRigidbody2D.velocity.y);
        time = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetIsInput(true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if (time > 0.2f && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            hostStateMachine.ChangeState<PlayerIdleBehavior>("Idle1");
        }

        if (player.isOnGround && isFinish)
        {
            hostStateMachine.ChangeState<PlayerIdleBehavior>("Idle1");
        }
        else if (!player.isOnGround && isFinish)
        {
            hostStateMachine.ChangeState<PlayerFallBehavior>("Fall1");
        }
    }
}

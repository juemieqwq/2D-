using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : PlayerBaseState
{
    bool continuousAttack;
    int currentAttacknum;

    //用于跳过帧几帧
    int skipFrames;

    public override void Enter()
    {
        skipFrames = 2;
        isLoop = false;
        PlayerInit();
        hostRigidbody2D.velocity = new Vector2(0, hostRigidbody2D.velocity.y);
        base.Enter();
        continuousAttack = false;
        currentAttacknum = 1;
    }

    public override void Exit()
    {
        player.SetIsInput(true);
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (skipFrames > 0)
        {
            skipFrames--;
            return;
        }
        if (controller.mouse0.isPressed)
            continuousAttack = true;
        if (isFinish && !continuousAttack)
        {
            hostStateMachine.ChangeState<PlayerIdleBehavior>("Idle1");
        }
        else if (isFinish && continuousAttack)
        {
            currentAttacknum++;
            var key = string.Concat("Attack", currentAttacknum);
            if (hostAnimator.DicPlayImagesGameObjects.GetValueOrDefault(key) != null)
            {
                hostAnimator.PlayRoleBehavior(key, isLoop);
                continuousAttack = false;
            }
            else
            {
                currentAttacknum = 1;
                key = string.Concat("Attack", currentAttacknum);
                hostAnimator.PlayRoleBehavior(key, isLoop);
                continuousAttack = false;
            }

        }

    }
}

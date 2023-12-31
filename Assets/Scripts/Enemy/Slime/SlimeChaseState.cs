using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChaseState : BaseState
{
    private Slime slime;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        slime = (Slime)currentEnemy;
        slime.currentSpeed = slime.chaseSpeed;
        slime.jumpForce = slime.jumpEnhancedForce;
    }
    public override void LogicUpdate()
    {
        if (slime.isDead)
            return;
        if (slime.lostCounter <= 0)
        {
            slime.SwitchState(NPCState.Patrol);
            return;
        }

        if (slime.transform.position.x >= slime.slimeBound.bounds.max.x && slime.sr.flipX == true
        || slime.transform.position.x <= slime.slimeBound.bounds.min.x && slime.sr.flipX == false)
            slime.sr.flipX = !slime.sr.flipX;
    }
    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

}

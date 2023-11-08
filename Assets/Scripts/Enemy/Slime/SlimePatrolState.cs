using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatrolState : BaseState
{
    private Slime slime;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        slime = (Slime)currentEnemy;
        slime.currentSpeed = slime.normalSpeed;
        slime.jumpForce = slime.jumpNormalForce;
    }
    public override void LogicUpdate()
    {
        if (slime.FoundPlayer())
        {
            slime.SwitchState(NPCState.Chase);
            return;
        }

        // ! may exist BUG: hurt will change the face dir, so the bound cannot make sense.
        // ? how should we handle hitting the bound when jumping?
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

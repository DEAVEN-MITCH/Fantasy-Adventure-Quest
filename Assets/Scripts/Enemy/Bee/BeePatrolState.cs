using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private Bee bee;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        bee = (Bee)currentEnemy;
        bee.currentSpeed = bee.normalSpeed;
    }

    public override void LogicUpdate()
    {
        if (bee.FoundPlayer())
        {
            bee.SwitchState(NPCState.Chase);
            // Debug.Log("found!");
            return;
        }
        // TODO: need to handle face direction(only two is OK)
        if (bee.targetPoint.x - bee.transform.position.x >= 0)
            bee.sr.flipX = true;
        else
            bee.sr.flipX = false;
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPatrolState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.anim.SetBool("walk", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            return;
        }
        if (!currentEnemy.pc.isGround || currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1)
        {
            //currentEnemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("patrol");
            //if (!currentEnemy.pc.isGround ) Debug.Log(" ");else Debug.Log("");
            currentEnemy.wait = true;
            //Debug.Log("no walk?");
            currentEnemy.anim.SetBool("walk", false);
            currentEnemy.anim.SetBool("run", false);
        }
        else
        {
            //Debug.Log("go!");
            currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}

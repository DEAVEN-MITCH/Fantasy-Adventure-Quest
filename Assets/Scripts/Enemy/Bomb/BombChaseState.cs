using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class BombChaseState : BaseState
{
    private Bomb bomb;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        bomb = (Bomb)enemy;
        bomb.anim.SetBool("run", true);
        bomb.currentSpeed = currentEnemy.chaseSpeed;
    }
    public override void LogicUpdate()
    {
        float distance = Vector3.Distance(bomb.transform.position, bomb.players.transform.position);
        if (System.Math.Abs(bomb.transform.position.x - bomb.aim.x) <= bomb.aim_x || distance<1)
        {
            bomb.bombing = true;
            bomb.SwitchState(NPCState.Halt);
            return;
        }
        if (!currentEnemy.pc.isGround || currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1)
        {
            bomb.bombing = true;
            bomb.SwitchState(NPCState.Halt);
            return;
        }
    }

    public override void OnExit()
    {
        /*        Debug.Log("bombing");*/
        if (bomb.bombing == false)
        {
            currentEnemy.anim.SetBool("run", false);
            return; 
        }
        bomb.currentSpeed = 0;
        bomb.bombing = true;
        bomb.bomb1.SetActive(true);
        bomb.anim.SetBool("run", false);
        /*            bomb.anim.SetBool("bomb", true);*/
        currentEnemy.anim.SetBool("run", false);
        bomb.anim.SetBool("dead", true);
        
        bomb.Die();
    }

    public override void PhysicsUpdate()
    {
    }
}

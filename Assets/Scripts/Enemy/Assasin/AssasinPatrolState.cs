using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinPatrolState : BaseState
{
    private Assasin assasin;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        assasin = (Assasin)enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed * assasin.speedParameter;
    }

    public override void LogicUpdate()
    {
        if(assasin.stage == 1 && currentEnemy.character.currentHealth < currentEnemy.character.maxHealth * 0.5f && !currentEnemy.isHurt && !assasin.isAttack)
        {
            assasin.isHinding = true;
            currentEnemy.character.TriggerInvulnerable();
            currentEnemy.anim.SetTrigger("hide");
            currentEnemy.SwitchState(NPCState.Skill);
        }
        if (currentEnemy.FoundPlayer() && !currentEnemy.wait)
        {
            currentEnemy.SwitchState(NPCState.Chase);
            return;
        }
        if (!currentEnemy.pc.isGround||currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1 || currentEnemy.wait)
        {
            currentEnemy.wait = true;
            //Debug.Log("no walk?");
            currentEnemy.anim.SetBool("walk", false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinChaseState : BaseState
{
    private Assasin assasin;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        assasin = (Assasin)enemy;
        enemy.anim.SetBool("walk", true);
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostCounter = currentEnemy.lostTime;
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
        if (currentEnemy.lostCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
            return;
        }
        if (!currentEnemy.pc.isGround || currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1)
        {
            currentEnemy.sr.flipX = !currentEnemy.sr.flipX;
        }
        if (assasin.playerInDistance)
        {
            //attack
            assasin.isAttack = true;
            currentEnemy.anim.SetBool("isAttack", assasin.isAttack);
            currentEnemy.anim.SetTrigger("attack");
            currentEnemy.SwitchState(NPCState.Skill);
            return;
        }
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);

    }

    public override void PhysicsUpdate()
    {
    }
}

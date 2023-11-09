using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinSkillState : BaseState
{
    private Assasin assasin;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        assasin = (Assasin)enemy;
        currentEnemy.currentSpeed = 0;
    }

    public override void LogicUpdate()
    {
        if(assasin.isHinding)
            currentEnemy.character.TriggerInvulnerable();
        
        if(!assasin.isAttack && !assasin.isHinding)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
    }
}

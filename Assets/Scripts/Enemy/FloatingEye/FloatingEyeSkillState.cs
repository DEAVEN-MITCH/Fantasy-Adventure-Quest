using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEyeSkillState : BaseState
{
    FloatingEye floatingEye;
    public override void LogicUpdate()
    {

        
        if (floatingEye.attackCount > 0)// cooling down
        {

            if (floatingEye.hurtSignal)//when cooling down get hurt reset the counter and RandomMove
            {
                floatingEye.attackCount = floatingEye.attackInterval;
                floatingEye.hurtSignal = false;
                floatingEye.RandomMove();
            }
            if (!floatingEye.FoundPlayer())//when cooling down the player is missing 
            {
                currentEnemy.SwitchState(NPCState.Patrol);
                return;
            }
            floatingEye.attackCount = Mathf.Max(0, floatingEye.attackCount - Time.deltaTime);
            if(!floatingEye.isPreAttack)floatingEye.DirectionFollowPlayer();
            if (floatingEye.attackCount == 0)//cool down,get into preattack state
            {
                floatingEye.preAttackCount = floatingEye.preAttackInterval;//Ç°Ò¡
                floatingEye.PreAttack();

            }
        }
        if(floatingEye.attackCount==0)//cool down,count down preAttackCount,
        {
            if (floatingEye.hurtSignal)//when preAttack get hurt  revoke preattack and recool down¡¢random move
            {
                floatingEye.hurtSignal = false;
                floatingEye.DePreAttack();
                floatingEye.RandomMove();
                floatingEye.attackCount = floatingEye.attackInterval;
                return;
            }
            if (floatingEye.preAttackCount > 0)
            {
                floatingEye.preAttackCount = Mathf.Max(0, floatingEye.preAttackCount - Time.deltaTime);
            }
            if(floatingEye.preAttackCount==0)//preAttackCount count over,attack and recool down
            {
                floatingEye.Attack();
                floatingEye.attackCount = floatingEye.attackInterval;//go back to cool down
            }
        }
    }

    public override void OnEnter(Enemy enemy)
    {

        currentEnemy = enemy;
        floatingEye = (FloatingEye)enemy;
        currentEnemy.lostCounter = currentEnemy.lostTime;
        floatingEye.attackCount = floatingEye.attackInterval;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {
        //debug
       //floatingEye.currentHeightAboveTheGround(floatingEye.transform.position);
    }

}

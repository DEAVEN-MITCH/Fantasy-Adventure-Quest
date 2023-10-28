using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class BeeChaseState : BaseState
{
    private Bee bee;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        bee = (Bee)currentEnemy;
        bee.anim.SetBool("isAttack", true);
        bee.currentSpeed = bee.chaseSpeed;
        bee.lostCounter = bee.lostTime;
        bee.shootCount = bee.shootInterval;         // reset it to maximum interval
    }

    public override void LogicUpdate()
    {
        if (bee.lostCounter <= 0)
        {
            bee.SwitchState(NPCState.Patrol);
            // Debug.Log("lost!");
            return;
        }
        Vector3 playerPosition = bee.players[0].transform.position;
        // TODO: once you have escaped the attack range, lost immediately.
        if (Vector2.Distance(playerPosition, bee.transform.position) > bee.attack.attackRange)
        {
            bee.lostCounter = 0;
            return;
        }
        if (bee.shootCount > 0)
            bee.shootCount = Mathf.Max(bee.shootCount - Time.deltaTime, 0);
        else
        {
            bee.anim.SetTrigger("attack");
            Shoot();
        }

        if (playerPosition.x - bee.transform.position.x >= 0)
            bee.sr.flipX = true;
        else
            bee.sr.flipX = false;
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        bee.anim.SetBool("isAttack", false);
        bee.transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0, 0, 0));
    }

    private void Shoot()
    {
        if (bee.shootCount <= 0)
        {
            Vector3 playerPosition = bee.players[0].transform.position;

            // TODO: find the player and calculate the direction towards him
            // TODO: it is significant to notice that how to handle different flipX
            Vector3 direction = (playerPosition - bee.transform.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, direction);
            bulletRotation *= Quaternion.Euler(0, 0, 90);

            Object.Instantiate(bee.bulletPrefab, bee.transform.position + bee.bulletOffset, bee.transform.rotation * bulletRotation);
            // Debug.Log("shot!");
            bee.shootCount = bee.shootInterval;
        }
    }
}

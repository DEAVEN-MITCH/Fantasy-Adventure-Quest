using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoar : Enemy
{
    public AudioSource roar;
    //public float timeToMaxChaseSpeed;
    override protected void Awake()
    {
        patrolState = new BlackBoarPatrolState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        b2.offset = new Vector2(0, c2.offset.y - c2.size.y / 2 + b2.size.y / 2);
        chaseState = new BlackBoarChaseState();
        haltState = new BlackBoarHaltState();
    }
    public override void TakeDamage(Transform attackTrans)
    {
        if (currentState is BlackBoarChaseState)
        {
            //attacker = attackTrans;
        }
        else if (currentState is BlackBoarHaltState)
        {
            rb.velocity = Vector2.zero;
            attacker = attackTrans;
            isHurt = true;
            anim.SetTrigger("hurt");
            Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        }
        else
        {
            base.TakeDamage(attackTrans);
        }
    }
}

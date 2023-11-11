using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    private Attack attack;
    public GameObject players;
    RaycastHit2D hit;
    public Vector2 aim;
    public Vector2[] pointsForhit;
    public float flyTime = 5;
    public GameObject bulletPrefab;
    public float throwtime = 2f;
    public float cd;
    public float gravity = 9.8f;
    public float speed_X = 9.8f;
    public float random = 3f;
    protected override void Awake()
    {
        patrolState = new SnailPatrolState();
        chaseState = new SnailChaseState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        b2.offset = new Vector2(0, c2.offset.y - c2.size.y / 2 + b2.size.y / 2);
        currentSpeed = 0;
        attack = GetComponent<Attack>();
    }
    void Start()
    {


    }
    public override void TakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        isHurt = true;
        anim.SetTrigger("hurt");
        //no force considering the randommove
        //Vector3 dir = (transform.position-attackTrans.position).normalized;
        //rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        //RandomMove();not called here in case the Attack ray moves before it is destroyed

    }
/*    private void RandomMoveAmongPoints()
    {
        int size = pointsForMovememt.Length;
        int randomIndex = UnityEngine.Random.Range(0, size);
        transform.position = pointsForMovememt[randomIndex];
        return;
    }*/
}

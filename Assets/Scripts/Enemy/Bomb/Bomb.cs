using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Enemy
{
    //public float timeToMaxChaseSpeed;
    public GameObject players;
    RaycastHit2D hit;
    public Vector3 aim;
    public float aim_x; //��ַƫ����
    public bool bombing;
    public GameObject bomb1;
    public AudioSource explode;
    override protected void Awake()
    {
        bomb1.SetActive(false);
        bombing = false;
        patrolState = new BombPatrolState();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        b2.offset = new Vector2(0, c2.offset.y - c2.size.y / 2 + b2.size.y / 2);
        chaseState = new BombChaseState();
        haltState = new BlackBoarHaltState();
        base.Awake();
    }
    public override void TakeDamage(Transform attackTrans)
    {
/*        Debug.Log("hurt");*/
        if (currentState is BombChaseState)
        {
/*            Debug.Log("no hurt");*/
            //attacker = attackTrans;
        }
        else
        {
            Debug.Log("hurting");
            base.TakeDamage(attackTrans);
        }
    }
    public override bool FoundPlayer()
    {
        hit = Physics2D.BoxCast((Vector2)transform.position + centerOffset * faceDir, checkSize, 0, faceDir, checkDistance, attackLayer);
        if (hit.collider != null)
        { aim = hit.transform.position;
        }
        return hit;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    public AudioSource roar;
    override protected void Awake()
    {
        patrolState = new BoarPatrolState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        b2.offset = new Vector2(0, c2.offset.y - c2.size.y / 2+b2.size.y/2);
        chaseState = new BoarChaseState();
    }
}

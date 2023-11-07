using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Enemy
{
    public GameObject player;
    public float attackDistance;
    public bool playerInDistance;

    override protected void Awake()
    {
        patrolState = new AssasinPatrolState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        chaseState = new AssasinChaseState();
        skillState = new AssasinSkillState();
        playerInDistance = false;
    }

    override protected void FixedUpdate() {
        if (faceDir.x == 1 && GetComponent<Transform>().position.x - player.transform.position.x <= 0 && player.transform.position.x - GetComponent<Transform>().position.x <= attackDistance || faceDir.x == -1 && GetComponent<Transform>().position.x - player.transform.position.x >= 0 && GetComponent<Transform>().position.x - player.transform.position.x<= attackDistance)
            playerInDistance = true;
        else
            playerInDistance = false;
    }
}

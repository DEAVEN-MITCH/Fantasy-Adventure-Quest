using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Enemy
{
    public GameObject player;
    public float attackDistance;
    public bool playerInDistance;
    public bool isAttack;
    public bool isHinding;
    public int stage;

    override protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        pc = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();
        character = GetComponent<Character>();
        waitCounter = waitTime;

        patrolState = new AssasinPatrolState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        chaseState = new AssasinChaseState();
        skillState = new AssasinSkillState();
        playerInDistance = false;
        stage = 1;
    }

    override protected void FixedUpdate() {
        if (!wait && !isHurt)
        {
            Move();
            //Debug.Log("just move!");
        }

        currentState.PhysicsUpdate();
        if (transform.position.y < -1000 && !isDead)
        {
            isDead = true;
            Die();
        }

        if (faceDir.x == 1 && GetComponent<Transform>().position.x - player.transform.position.x <= 0 && player.transform.position.x - GetComponent<Transform>().position.x <= attackDistance || faceDir.x == -1 && GetComponent<Transform>().position.x - player.transform.position.x >= 0 && GetComponent<Transform>().position.x - player.transform.position.x<= attackDistance)
            playerInDistance = true;
        else
            playerInDistance = false;

        anim.SetBool("isAttack", isAttack);
        anim.SetInteger("stage", stage);
    }
    
    override public void Die()
    {
        //Debug.Log(this + "dies!");
        System.Random ran = new System.Random();
        for (int i = 0; i < dropItem.Length; i++)
        {
            int n = ran.Next(100);
            //Debug.Log(n);
            if (n < dropProbability[i] /*&& looted == 0*/)
            {
                float xBias = (ran.Next(100) - 50) / 100f;
                Vector3 location = new Vector3(this.gameObject.transform.position.x + xBias, this.gameObject.transform.position.y + 1.5f, 0);
                Instantiate(dropItem[i], location, this.gameObject.transform.rotation);
            }
        }
        //looted = 1;
        this.gameObject.layer = 2;
        changeScore?.Invoke(scoreValue);
        anim.SetTrigger("dead");
        currentSpeed = 0;
    }
}

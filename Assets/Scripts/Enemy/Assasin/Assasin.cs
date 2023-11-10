using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Enemy
{
    private GameObject player;
    private WarningSign warningSign;
    public float attackDistance;
    public bool playerInDistance;
    public bool isAttack;
    public bool isHinding;
    public int stage;

    //speed related
    public float speedInStage2;
    public float speedParameter;
    
    public Vector2[] pointsForMovememt;

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
        speedParameter = 1;
        player = GameObject.Find("player");
        warningSign = player.transform.Find("WarningArea").GetComponent<WarningSign>();
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

        if(stage == 2)
        {
            speedParameter = speedInStage2;
            if(FoundPlayer())
                warningSign.invisibleEnemyNum += 1;
        }
        else
            speedParameter = 1f;
            
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

    public void Teleport()
    {
        int size = pointsForMovememt.Length;
        int randomIndex = UnityEngine.Random.Range(0, size);
        transform.position = pointsForMovememt[randomIndex];
        return;
    }
}

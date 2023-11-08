using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck pc;
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Character character;

    [Header("Basic Parameters")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Transform attacker;
    public int scoreValue;
    [Header("Check Attributes")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public LayerMask attackLayer;
    public float checkDistance;

    [Header("Time Counter")]
    public bool wait;
    public float waitTime;
    public float waitCounter;
    public float lostTime;
    public float lostCounter;
    [Header("State")]
    public bool isHurt;
    public bool isDead = false;
    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState haltState;
    protected BaseState skillState;
    public int[] dropProbability; //unit:%
    public GameObject[] dropItem;
    private int looted;
    public UnityEvent<int> changeScore;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        pc = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();
        character = GetComponent<Character>();
        waitCounter = waitTime;
    }
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
        looted = 0;
    }
    // Update is called once per frame
    protected void Update()
    {
        faceDir = new Vector3(sr.flipX ? 1 : -1, 0, 0);

        currentState.LogicUpdate();
        TimeCounter();

        //Debug.Log(faceDir);
    }
    virtual public void Move()
    {
        rb.velocity = new Vector2(character.speedCorrection * currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    virtual protected void FixedUpdate()
    {
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
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public void TimeCounter()
    {
        if (!FoundPlayer() && lostCounter > 0)
        {
            lostCounter -= Time.deltaTime;
        }
        else if (FoundPlayer())
        {
            lostCounter = lostTime;
        }
        if (wait)
        {
            //if (!isHurt) { rb.velocity = Vector2.zero; }
            if (!isHurt) { rb.velocity = new Vector2(0, rb.velocity.y); }
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                wait = false;
                waitCounter = waitTime;
                sr.flipX = pc.touchLeftWall ? true : pc.touchRightWall ? false : !pc.isGround ? !sr.flipX : sr.flipX;
            }
        }
    }
    virtual public bool FoundPlayer()
    {
        return Physics2D.BoxCast((Vector2)transform.position + centerOffset * faceDir, checkSize, 0, faceDir, checkDistance, attackLayer);
    }
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Halt => haltState,
            NPCState.Skill => skillState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #region Event
    public virtual void TakeDamage(Transform attackTrans)
    {
        rb.velocity = Vector2.zero;
        // Debug.Log("hurt");
        attacker = attackTrans;
        if (attackTrans.position.x - transform.position.x < 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    virtual public void Die()
    {
        //Debug.Log(this + "dies!");
        System.Random ran = new System.Random();
        for (int i = 0; i < dropItem.Length; i++)
        {
            int n = ran.Next(100);
            //Debug.Log(n);
            if (n < dropProbability[i] && looted == 0)
            {
                float xBias = (ran.Next(100) - 50) / 100f;
                Vector3 location = new Vector3(this.gameObject.transform.position.x + xBias, this.gameObject.transform.position.y, 0);
                Instantiate(dropItem[i], location, this.gameObject.transform.rotation);
            }
        }
        looted = 1;
        this.gameObject.layer = 2;
        changeScore?.Invoke(scoreValue);
        anim.SetTrigger("dead");
    }

    #endregion Event
    virtual protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)(centerOffset), .2f);
        //Gizmos.DrawWireSphere((Vector2)transform.position + pc.bottomOffset, .2f);
    }
}

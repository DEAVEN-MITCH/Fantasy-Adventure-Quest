using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector]public PhysicsCheck pc;
    [HideInInspector]public SpriteRenderer sr;
    
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Transform attacker;
    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public LayerMask attackLayer;
    public float checkDistance;

    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCounter;
    public float lostTime;
    public float lostCounter;
    [Header("状态")]
    public bool isHurt;
    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState haltState;

    protected virtual  void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        pc = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();
        waitCounter = waitTime;
    }
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(sr.flipX?1:-1, 0, 0);
        
        currentState.LogicUpdate();
        TimeCounter();

        //Debug.Log(faceDir);
    }
    virtual public void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    private void FixedUpdate()
    {
        if (!wait&&!isHurt)
        {
            Move();
            //Debug.Log("just move!");
        }
        //Debug.Log("?????1?");
        currentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public void TimeCounter()
    {
        if (!FoundPlayer()&&lostCounter>0)
        {
            lostCounter -= Time.deltaTime;
        }else if (FoundPlayer())
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
    public bool FoundPlayer()
    {
        return Physics2D.BoxCast((Vector2)transform.position + centerOffset*faceDir, checkSize, 0, faceDir, checkDistance, attackLayer);
    }
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Halt=>haltState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #region 事件
    public virtual void  TakeDamage(Transform attackTrans)
    {
        rb.velocity = Vector2.zero;
        //Debug.Log("hurt");
        attacker = attackTrans;
        if(attackTrans.position.x - transform.position.x < 0)
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
        rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);
    }
    public void Die()
    {
        this.gameObject.layer = 2;
        anim.SetTrigger("dead");
    }
    #endregion 事件
    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)(centerOffset ), .2f);
        //Gizmos.DrawWireSphere((Vector2)transform.position + pc.bottomOffset, .2f);
    }
}

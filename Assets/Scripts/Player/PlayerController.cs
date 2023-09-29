using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    public Player inputControl;
    public Vector2 inputDirection;
    //public Transform AttackArea;
    private Rigidbody2D rb;
    private PhysicsCheck PCheck;// = GetComponent<PhysicsCheck>();
    private Playeranimation pa;
    [Header("基本参数")]
    public float speed;
    public float hurtForce;
    public float jumpForce;
    [Header("基本状态")]
    public bool isHurt,isDead;
    public bool isAttack;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    private void Awake()
    {
        inputControl = new Player();
        rb = GetComponent<Rigidbody2D>();
        inputControl.Gameplay.Jump.started += Jump;
        PCheck = GetComponent<PhysicsCheck>();
        inputControl.Gameplay.Attack.started += DoAttack;
        pa = GetComponent<Playeranimation>();
        normal = new PhysicsMaterial2D("Normal");
        wall = new PhysicsMaterial2D("Wall");
    }



    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    //Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        if (transform.position.y < -50)
        {
            transform.position =new Vector3(-0.5112553f, 3, 0f);
        }
    }
    private void FixedUpdate()
    {
        if (!isHurt&& !isAttack)
        {
            Move();
        }
        CheckState();
    }
    public void Move()//ASDsAdas 
    {
        rb.velocity = new Vector2(inputDirection.x* speed * Time.deltaTime, rb.velocity.y);

        //人物翻转
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.flipX = rb.velocity.x < 0 || (rb.velocity.x<=0 && sr.flipX);
        transform.Find("Attack Area").transform.localScale = sr.flipX ? new Vector3(-1, 1, 1) :new Vector3 (1, 1, 1);
}
    private void Jump(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
        if(PCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;
        rb.velocity = Vector2.zero;
        //Debug.Log(dir);
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void Dead()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        inputControl.Gameplay.Disable();
    }
    private void DoAttack(InputAction.CallbackContext obj)
    {
        pa.PlayAttack();
        isAttack = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void CheckState()
    {
        rb.sharedMaterial = PCheck.isGround ? normal : wall;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    //public Transform AttackArea;
    private Rigidbody2D rb;
    private PhysicsCheck PCheck;// = GetComponent<PhysicsCheck>();
    private PlayerAnimation pa;
    private SpriteRenderer sr;
    [Header("Events")]
    public UnityEvent afterDeathAnimation;
    public UnityEvent<float> onPowerChange;
    [Header("Basic Parameters")]
    public float speed;
    public float hurtForce;
    public float jumpForce;
    public float maxPower;
    public float currentPower;
    public float shootInterval;
    public float shootCount;
    public Vector3 bulletOffset;//1,0.9,0
    [Header("Basic Status")]
    public bool isHurt, isDead;
    public bool isAttack;
    public bool isDoubleJumpUnlocked;
    public int jumpCounter;
    [Header("Physics Material")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("Bullet Prefab")]
    public GameObject bulletPrefab;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        inputControl.Gameplay.Jump.started += Jump;
        PCheck = GetComponent<PhysicsCheck>();
        inputControl.Gameplay.Attack.started += DoAttack;
        inputControl.Gameplay.RangedAttack.started += RangedAttack;
        pa = GetComponent<PlayerAnimation>();
        normal = new PhysicsMaterial2D("Normal");
        wall = new PhysicsMaterial2D("Wall");
        jumpCounter = 1;
        isDoubleJumpUnlocked = false;
        currentPower = maxPower;
        sr = GetComponent<SpriteRenderer>();
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
        onPowerChange.Invoke(currentPower / maxPower);
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCount > 0)
        {
            shootCount = Mathf.Max(shootCount - Time.deltaTime, 0);
        }
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        if (transform.position.y < -50)
        {
            //transform.position = new Vector3(-0.5112553f, 3, 0f);
            GetComponent<Character>().OnDie.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
        else if (isAttack)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (PCheck.isGround)
            jumpCounter = 1;
        CheckState();
    }
    public void Move()//ASDsAdas 
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //人物翻转
        sr.flipX = rb.velocity.x < 0 || (rb.velocity.x <= 0 && sr.flipX);
        transform.Find("Attack Area").transform.localScale = sr.flipX ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (PCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter = 1;
        }
        else if (isDoubleJumpUnlocked && jumpCounter > 0)
        {
            // set velocity to 0, so the jump effect could be the same
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter = 0;
        }
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
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
    public void AfterDeadthAnimation()
    {
        afterDeathAnimation.Invoke();
    }
    private void RangedAttack(InputAction.CallbackContext obj)
    {
        float powerComsumption = bulletPrefab.GetComponent<Bullet>().powerConsumption;
        if (currentPower >= powerComsumption && shootCount == 0)
        {
            Vector3 dir = new(0, sr.flipX ? 180 : 0, 0);
            Vector3 offset = new(sr.flipX ? -bulletOffset.x : bulletOffset.x, bulletOffset.y, bulletOffset.z);
            Instantiate(bulletPrefab, transform.position + offset, Quaternion.Euler(dir));
            currentPower -= powerComsumption;
            //Debug.Log("shot!");
            onPowerChange.Invoke(currentPower / maxPower);
            shootCount = shootInterval;
        }
        else
        {
            //not enough power
        }
    }
}

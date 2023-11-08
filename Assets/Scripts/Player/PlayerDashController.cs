using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerDashController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Character character;
    private PlayerController pc;
    private PlayerHealController phc;
    private PlayerDashAnimation pda;
    private Rigidbody2D rb;
    private PhysicsCheck PCheck;


    [Header("Dash����")]
    public float dashTime;//dashʱ��
    public float dashTimeLeft;//dashʣ��ʱ��
    public float currentCoolDown;
    public float dashCoolDown;
    public float dashSpeed;
    public bool isDashing;

    //˫��A/D��������̶���
    public float maxAwaitTime;
    private bool moving, canDash;

    public float walkSpeed;
    private float currentSpeed;

    private float h;
    SpriteRenderer sr;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        phc = GetComponent<PlayerHealController>();
        pda = GetComponent<PlayerDashAnimation>();
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        PCheck = GetComponent<PhysicsCheck>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        inputControl = pc.inputControl;
        inputControl.Gameplay.Dash.started += Dash;
    }

    private void FixedUpdate()
    {

        h = sr.flipX ?-1 : 1;
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                if (!PCheck.isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * h, 0);
                }
                else
                    rb.velocity = new Vector2(dashSpeed * h, 0);
                dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFromPool();
            }
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                if (!PCheck.isGround)
                {
                    //rb.velocity = new Vector2(dashSpeed * h, pc.jumpForce);
                }
                rb.gravityScale = 4;
                character.invulnerable = false;
            }
        }

        if (currentCoolDown > 0)
            currentCoolDown -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        currentCoolDown = dashCoolDown;
        rb.gravityScale = 0;
    }

    public void Dash(InputAction.CallbackContext obj)
    {
        Debug.Log("Dash");

        if (!isDashing && currentCoolDown <= 0 && !pc.isHurt && !phc.isHeal)
        {
            ReadyToDash();
            pda.PlayDash();
            character.TriggerInvulnerable();
        }
    }
}


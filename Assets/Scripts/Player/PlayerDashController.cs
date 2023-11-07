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
    

    [Header("Dash参数")]
    public float dashTime;//dash时长
    public float dashTimeLeft;//dash剩余时间
    public float lastDash = -10f;//上一次dash时间点
    public float dashCoolDown;
    public float dashSpeed;
    public bool isDashing;

    //双击A/D键触发冲刺动作
    public float maxAwaitTime;
    private float leftPressTime, rightPressTime;
    private bool moving, canDash;

    public float walkSpeed;
    private float currentSpeed;

    private float h;
    

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        phc = GetComponent<PlayerHealController>();
        pda = GetComponent<PlayerDashAnimation>();
        character = GetComponent<Character>();
        leftPressTime = rightPressTime = -maxAwaitTime;//令左右键按下的时间都初始化为负的最大等待时间
    }


    void Start()
    {
        inputControl = pc.inputControl;
        //inputControl.Gameplay.Dash.started += Dash;
    }

    private void FixedUpdate()
    {
        //Dash();

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        changeFaceDirection();
        //Dash();
        Dash();
        if (isDashing)
            return;
    }

    void changeFaceDirection()
    {
        if(h == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(h == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /*void checkDash()
    {
        if(h == 1 && !moving)
        {
            if(Time.time - rightPressTime <= maxAwaitTime)
            {
                canDash = true;
            }
            rightPressTime = Time.time;
        }
        if (h == -1 && !moving)
        {
            if (Time.time - leftPressTime <= maxAwaitTime)
            {
                canDash = true;
            }
            leftPressTime = Time.time;
        }

    }*/

    public void Dash()
    {
           Debug.Log("Dash");
           

           if (isDashing)
          {
                if(dashTimeLeft > 0)
                {
                        
                     pc.Dash();
                     dashTimeLeft -= Time.deltaTime;
                     ShadowPool.instance.GetFromPool();
                }
           }
           if(dashTimeLeft <= 0)
          {
                isDashing = false;
           }

                 /*if (Mathf.Abs(h) == 1)
                 {
                     moving = true;
                     if (canDash)
                     {
                          isDashing = true;
                         currentSpeed = dashSpeed;
                         pda.PlayDash();//改步播放冲刺动画即使用对象池
                      }
                     else
                     {
                          isDashing = false;
                          currentSpeed = walkSpeed;
                          pc.Move();
                      }
                 }
                else
                {
                    isDashing = false;
                    pda.SetAnimation();
                    moving = false;
                    canDash = false;
                 }

                pda.PlayDash();*/
            
        

            
    }



    public float getDashTimeLeft()
    {
        return dashTimeLeft;
    }

    public float geth()
    {
        return h;
    }

    public void PlayDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                pc.UpSpeed(h, currentSpeed);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.GetFromPool();
            }
        }
        if(dashTimeLeft <= 0)
        {
            isDashing = false;
        }
        //anim.SetTrigger("dash");
    }

    public void ReadyToDash()
    {
        isDashing = pc.isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }

    /*public void Dash()
    {
        Debug.Log("Dash");


        if (isDashing)
        {
            if (pdc.dashTimeLeft > 0)
            {
                if (rb.velocity.y > 0 && !PCheck.isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * pdc.geth(), jumpForce);
                }
                rb.velocity = new Vector2(dashSpeed * pdc.geth(), rb.velocity.y);
                pdc.dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFromPool();
            }
            if (pdc.dashTimeLeft <= 0)
            {
                isDashing = false;
                if (!PCheck.isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * pdc.geth(), jumpForce);
                }
            }
        }

    }*/
}


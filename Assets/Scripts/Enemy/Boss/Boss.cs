using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool isTeleport;
    public BossState lastAttackState;  //Default: wait
    protected BaseState waitState;
    protected BaseState chargeState;
    protected BaseState barrage1State;
    protected BaseState barrage2State;
    protected BaseState nebulaState;
    protected BaseState rockFallState;
    protected BaseState brillianceState;

    [Header("Bullet Prefabs")]
    public GameObject barrage1;
    public GameObject barrage2;
    public GameObject nebula;
    public GameObject nebulaSmall;
    public GameObject rock;
    public GameObject brilliance;
    public GameObject star;

    [Header("Temporary Parameters")]
    public Vector2 teleportPosition;

    override protected void Awake()
    {
        waitState = new BossWaitState();
        chargeState = new BossChargeState();
        barrage1State = new BossBarrage1State();
        barrage2State = new BossBarrage2State();
        nebulaState = new BossNebulaState();
        rockFallState = new BossRockFallState();
        brillianceState = new BossBrillianceState();
        base.Awake();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        isTeleport = false;

        // ? TEST
        // Teleport(new Vector2(-30,115));
        //Teleport(new Vector2(-40,105),new Vector2(-20,105));
    }

    override protected void OnEnable()
    {
        currentState = waitState;
        currentState.OnEnter(this);
    }

    public void SwitchBossState(BossState state)
    {
        var newState = state switch
        {
            BossState.Wait => waitState,
            BossState.Charge => chargeState,
            BossState.Barrage1 => barrage1State,
            BossState.Barrage2 => barrage2State,
            BossState.Nebula => nebulaState,
            BossState.RockFall => rockFallState,
            BossState.Brilliance => brillianceState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    /*
        @ Author: Zhang Zirui
        @ name: Teleport
        @ Description: Set the trigger and teleportPosition.
                       After finishing the animation, automatically transport to the position.
        @ Parameters:
            Vector2 position: The position teleports to.
    */
    public void Teleport(Vector2 position)
    {
        isTeleport = true;
        anim.SetTrigger("teleport");
        teleportPosition = position;
    }

    /*
        @ Author: Zhang Zirui
        @ name: Teleport
        @ Description: Set the trigger and teleportPosition.
                       After finishing the animation, automatically transport to the position.
                       The position will be randomly choosed between 2 given points.
        @ Parameters:
            Vector2 position1: a position candidate for destination;
            Vector2 position2: a position candidate for destination.
    */
    public void Teleport(Vector2 position1, Vector2 position2)
    {
        isTeleport = true;
        anim.SetTrigger("teleport");
        int choice = Random.Range(0,2);
        teleportPosition = (choice == 0) ? position1 : position2;
    }

    /*
        @ Author: Zhang Zirui
        @ name: ActualTeleport
        @ Description: The actual teleport function. Change the transform position.
                       This function will be called after animation finishes.
                       See StartTeleport.cs for details.
    */
    public void ActualTeleport()
    {
        transform.position = teleportPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public BossState lastAttackState;  //Default: wait
    protected BaseState waitState;
    protected BaseState chargeState;
    protected BaseState barrage1State;
    protected BaseState barrage2State;
    protected BaseState nebulaState;
    protected BaseState rockFallState;
    protected BaseState brillianceState;

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
}

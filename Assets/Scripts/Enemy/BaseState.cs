using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    /// <summary>
    /// called by Update
    /// </summary>
    public abstract void LogicUpdate();
    /// <summary>
    /// called by FixedUpdate
    /// </summary>
    public abstract void PhysicsUpdate();
    public abstract void OnExit();

}

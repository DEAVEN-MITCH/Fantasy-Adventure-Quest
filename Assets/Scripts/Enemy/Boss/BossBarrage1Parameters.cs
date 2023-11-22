using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrage1Parameters : MonoBehaviour
{
    public Vector2 teleportPosition;
    public float shotInterval;
    public int shotNumber;
    public float shotSpeed;
    public float shotRange;//function by Prefab's Attack's AttackRange
    public float laserWidth,laserLength,rotationRate;//rotaion unit is degree
    public Attack laserAttack;
}

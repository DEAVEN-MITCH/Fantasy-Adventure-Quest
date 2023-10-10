using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBullet : Bullet
{
    protected override void InitPlayer()
    {
        player = GameObject.Find("Gear");
    }
}

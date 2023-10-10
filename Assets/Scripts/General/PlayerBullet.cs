using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void InitPlayer()
    {
        player = GameObject.Find("player");
    }
}

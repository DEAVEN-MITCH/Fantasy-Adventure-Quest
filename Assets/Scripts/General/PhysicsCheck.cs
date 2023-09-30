using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    SpriteRenderer sr;
    [Header("������")]
    public bool manual;
    public bool isGround;
    //public bool isNearCliff;
    public bool touchLeftWall;
    public bool touchRightWall;
    public LayerMask GroundLayer;
    public float checkRadius;
    public Vector2 bottomOffset,leftOffset,rightOffset;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        bottomOffset = new Vector2(Math.Abs(bottomOffset.x), bottomOffset.y);
        if (!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x/2 + coll.offset.x) , coll.offset.y);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }
    private void Check()
    {
        //int xd =sr.flipX?-1;

        //isNearCliff=!Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset+new Vector2(0.05f,0), checkRadius, GroundLayer)
        //    || !Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(-0.05f, 0), checkRadius, GroundLayer); 
        isGround= Physics2D.OverlapCircle((Vector2)transform.position+new Vector2( sr.flipX? bottomOffset.x:-bottomOffset.x,bottomOffset.y), checkRadius, GroundLayer);
        touchLeftWall=Physics2D.OverlapCircle((Vector2)transform.position+leftOffset, checkRadius, GroundLayer);
        touchRightWall=Physics2D.OverlapCircle((Vector2)transform.position+rightOffset, checkRadius, GroundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(GetComponent<SpriteRenderer>().flipX ? bottomOffset.x : -bottomOffset.x, bottomOffset.y), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}

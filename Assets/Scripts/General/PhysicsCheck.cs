using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    SpriteRenderer sr;
    [Header("检测参数")]
    public bool manual;
    public bool isGround;
    //public bool isNearCliff;
    public bool touchLeftWall;
    public bool touchRightWall;
    public LayerMask GroundLayer;
    public float checkRadius;
    public Vector2 bottomOffset,leftOffset,rightOffset;
    [Tooltip("这个属性控制bottomOffset.x的要旋转多少度检测地面，用于适应各种坡度，单位为degree")]
    public float detectedSlopeAngle;
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
    private void OnEnable()
    {
        Check();
    }
    private void Check()
    {
        //int xd =sr.flipX?-1;

        //isNearCliff=!Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset+new Vector2(0.05f,0), checkRadius, GroundLayer)
        //    || !Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(-0.05f, 0), checkRadius, GroundLayer); 
        float detectedSlopeAngle2Rad = detectedSlopeAngle * Mathf.Deg2Rad;
        float directedOffsetX = sr.flipX ? bottomOffset.x : -bottomOffset.x;
        isGround = Physics2D.OverlapCircle((Vector2)transform.position+new Vector2(directedOffsetX*Mathf.Cos(detectedSlopeAngle2Rad),bottomOffset.y-Math.Abs(directedOffsetX)*Mathf.Sin(detectedSlopeAngle2Rad)), checkRadius, GroundLayer);
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
        float detectedSlopeAngle2Rad = detectedSlopeAngle * Mathf.Deg2Rad;
        float directedOffsetX = GetComponent<SpriteRenderer>().flipX ? bottomOffset.x : -bottomOffset.x;
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(directedOffsetX * Mathf.Cos(detectedSlopeAngle2Rad), bottomOffset.y - Math.Abs(directedOffsetX) * Mathf.Sin(detectedSlopeAngle2Rad)), checkRadius, GroundLayer);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(directedOffsetX * Mathf.Cos(detectedSlopeAngle2Rad), bottomOffset.y - Math.Abs(directedOffsetX) * Mathf.Sin(detectedSlopeAngle2Rad)), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}

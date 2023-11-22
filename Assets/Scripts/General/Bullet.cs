using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public bool touchLeftWall;
    public bool touchRightWall;
    public LayerMask GroundLayer;
    public float checkRadius;
    public Vector2 leftOffset, rightOffset;
    public float flyingSpeed;
    public float powerConsumption;
    private Vector3 originalPosition;
    private Attack at;
    public float destroyDelay;
    private Vector3 dir;
    public LayerMask enemyLayer;
    private void OnEnable()
    {
        originalPosition = transform.position;
        Check();
        at = GetComponent<Attack>();
        dir = transform.right;
        //Debug.Log("enable!");
    }
    private void Check()
    {
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, GroundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, GroundLayer);
    }
    // Update is called once per frame
    void Update()
    {

        Check();
    }
    private void FixedUpdate()
    {
        float check = Math.Abs(flyingSpeed);
        if (Vector3.Distance(transform.position, originalPosition) >= at.attackRange || touchLeftWall || touchRightWall)
        {
            /*Debug.Log("destroy!");*/
            Destroy(this.gameObject);
        }
        if (check > 0.1)
            GetComponent<Rigidbody2D>().velocity = flyingSpeed * Time.deltaTime * dir;

        //Debug.Log(player.GetComponent<Rigidbody2D>().velocity + "player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Destroy in 0.05s!");
        if (!collision.tag.Equals("player rebounce"))
        {
            //Debug.Log(string.Format("collision:{0}", collision));
            Destroy(this.gameObject, destroyDelay);//must has delay here so that the attack's OnTriggerStay2D can work
        }
        else
        {
            //Debug.Log("bullet touch rebounce");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
    public void ReboundedByPlayer()
    {
        dir = -dir;
        originalPosition = transform.position;
        var collider = GetComponent<BoxCollider2D>();
        collider.contactCaptureLayers = enemyLayer;
    }
}

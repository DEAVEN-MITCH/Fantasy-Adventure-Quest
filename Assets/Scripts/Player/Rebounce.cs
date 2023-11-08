using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebounce : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool isRebounce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision+"entered!");
        Bullet b;
        if ((b = collision.GetComponent<Bullet>() )!= null)
        {
            //Debug.Log("bullet entered!");
            bool t = b.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0;//shoot right so player should face left ie flipX=true
            if (sr.flipX==t)
            {
                b.ReboundedByPlayer();
            }
        }
    }
    private void Update()
    {
        var rebounceCollider = GetComponent<CircleCollider2D>();
        float x =Mathf.Abs( rebounceCollider.offset.x), y = rebounceCollider.offset.y;
        rebounceCollider.offset = new(sr.flipX?-x:x,y);
        //Debug.Log(string.Format("position:{0}",(Vector2)transform.position + rebounceCollider.offset));
    }
    private void OnDrawGizmosSelected()
    {
        var rebounceCollider = GetComponent<CircleCollider2D>();
        Gizmos.DrawWireSphere((Vector2)transform.position + rebounceCollider.offset, rebounceCollider.radius);
    }
}

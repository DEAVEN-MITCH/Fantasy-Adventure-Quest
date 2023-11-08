using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebounce : MonoBehaviour
{
    public SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b;
        if ((b = collision.GetComponent<Bullet>() )!= null)
        {
            bool t = b.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0;//shoot right so player should face left ie flipX=true
            if (sr.flipX==t)
            {
                b.ReboundedByPlayer();
            }
        }
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void DeActivate() {
        gameObject.SetActive(false);
    }
}

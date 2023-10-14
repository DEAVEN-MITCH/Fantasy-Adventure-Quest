using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    public int healthGenerate;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        System.Random ran = new System.Random();
        float force_x = (ran.Next(500) - 250) / 100f;
        float force_y = (ran.Next(500) + 500) / 100f;
        Vector2 force = new Vector2(force_x, force_y);
        //Debug.Log(force_x);
        //Debug.Log(force_y);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !(collision.GetComponent<Character>().HealthFull()))
        {
            collision.GetComponent<Character>()?.HealthRegen(healthGenerate);
            //Debug.Log("regen 10");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y < -1000)
        {
            Destroy(gameObject);
        }
    }
}

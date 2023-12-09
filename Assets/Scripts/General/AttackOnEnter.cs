using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnEnter : Attack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage((Attack)this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

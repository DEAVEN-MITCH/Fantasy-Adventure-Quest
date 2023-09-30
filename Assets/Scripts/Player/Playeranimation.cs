using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck pc;
    private PlayerController pr;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //anim.SetFloat("velocityX")
        pc = GetComponent<PhysicsCheck>();
        pr = GetComponent<PlayerController>();
    }
    public void SetAnimation()
    {
        float t = rb.velocity.x;
        if (t < 0) t = -t;
        anim.SetFloat("velocityX",t);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", pc.isGround);
        anim.SetBool("isDead", pr.isDead);
        anim.SetBool("isAttack", pr.isAttack);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}

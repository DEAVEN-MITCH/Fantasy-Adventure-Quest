using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerDashController pdc;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pdc = GetComponent<PlayerDashController>();
    }
    public void SetAnimation()
    {
        anim.SetBool("isDash", pdc.isDashing);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void PlayDash()
    {
       /* if (pdc.isDashing)
        {
            if(pdc.getDashTimeLeft() > 0)
            {
                pdc.pc.Up
            }
        }
        anim.SetTrigger("dash");*/
    }
}

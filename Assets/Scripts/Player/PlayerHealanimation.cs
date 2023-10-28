using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerHealController phr;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        phr = GetComponent<PlayerHealController>();
    }
    public void SetAnimation()
    {
        anim.SetBool("isHeal", phr.isHeal);
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
    public void PlayHeal()
    {
        anim.SetTrigger("heal");
    }
}

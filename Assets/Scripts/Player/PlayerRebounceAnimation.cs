using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRebounceAnimation: MonoBehaviour
{
    private Animator anim;
    private PlayerRebounceController prc;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        prc = GetComponent<PlayerRebounceController>();
    }
    public void SetAnimation()
    {
        anim.SetBool("isRebounce",prc.isRebounce);
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
}

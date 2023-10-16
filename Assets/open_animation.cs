using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_animation : MonoBehaviour
{
    private Animator animator;
    private TreasureTrigger tt;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        tt = GetComponent<TreasureTrigger>();
    }
    private void Update()
    {
        animator.SetBool("is_get", tt.isGet);
    }
}

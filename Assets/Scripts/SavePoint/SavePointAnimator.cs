using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isUsed", GetComponent<SaveGameManager>().isused);
    }
}

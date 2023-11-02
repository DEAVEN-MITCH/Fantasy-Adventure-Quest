using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFrostAnimation : MonoBehaviour
{
    private Animator anim;
    private Character chara;

    private void Awake() {
        anim = GetComponent<Animator>();
        chara = GetComponent<Character>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isFrost", chara.isFrost);
    }
}

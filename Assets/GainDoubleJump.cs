using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainDoubleJump : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController pc;
    void Start()
    {
        pc.isDoubleJumpUnlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

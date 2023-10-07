using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureTrigger : MonoBehaviour
{
    //private BoxCollider2D bc;
    private PlayerController pc;
    // Start is called before the first frame update
    //void Start()
    //{
    //    bc = GetComponent<BoxCollider2D>();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name + "enter");
        pc = collision.GetComponent<PlayerController>();
        pc.inputControl.Gameplay.InteractE.started += UnlockDoubleJump;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pc.inputControl.Gameplay.InteractE.started -= UnlockDoubleJump;
        //Debug.Log("exit");
    }
    private void UnlockDoubleJump(InputAction.CallbackContext obj)
    {
        pc.isDoubleJumpUnlocked = true;
        //Debug.Log(obj);
    }

    //    // Update is called once per frame
    //    void Update()
    //    {

    //    }
}

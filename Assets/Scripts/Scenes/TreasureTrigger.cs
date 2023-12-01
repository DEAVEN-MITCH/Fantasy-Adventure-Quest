using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TreasureTrigger : MonoBehaviour
{
    private PlayerController pc;
    public bool isGet = false;

    [Header("Hint Message")]
    public TextMeshProUGUI hintText;
    public UnityEvent<TextMeshProUGUI> onHintTrigger;
    private void Awake()
    {
        //gameObject.tag = "Chest";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGet)
            return;
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
        isGet = true;
        //Debug.Log(obj);

        // trigger a hint text
        onHintTrigger.Invoke(hintText);
    }
}




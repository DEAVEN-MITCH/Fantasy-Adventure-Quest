using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DashTreasure : MonoBehaviour
{
    private PlayerDashController pdc;
    public bool isGet = false;
    private Animator anim;
    [Header("Hint Message")]
    public TextMeshProUGUI hintText;
    public UnityEvent<TextMeshProUGUI> onHintTrigger;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("is_get", isGet);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGet)
            return;
        //Debug.Log(collision.name + "enter");
        pdc = collision.GetComponent<PlayerDashController>();
        pdc.inputControl.Gameplay.InteractE.started += UnlockDash;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pdc.inputControl.Gameplay.InteractE.started -= UnlockDash;
        //Debug.Log("exit");
    }

    private void UnlockDash(InputAction.CallbackContext obj)
    {
        pdc.isDashUnlocked = true;
        isGet = true;
        //Debug.Log(obj);

        // trigger a hint text
        onHintTrigger.Invoke(hintText);
    }
}




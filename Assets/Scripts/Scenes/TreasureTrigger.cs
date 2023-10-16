using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureTrigger : MonoBehaviour
{

    public float fadeSpeed = 1.0f;
    private PlayerController pc;
    public bool isGet = false;

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
/*    private void Update()
    {
*//*        while (isGet == true && renderer.material.color.a > 0)
        {
            timeElapsed += Time.deltaTime;
            renderer.material.color = Color.Lerp(currentColor, targetColor, timeElapsed * fadeSpeed);

            // 在达到目标透明度后销毁游戏对象
            if (renderer.material.color.a <= 0f)
            {
                Destroy(gameObject);
            }
        }*//*
    }*/
    private void UnlockDoubleJump(InputAction.CallbackContext obj)
    {
        pc.isDoubleJumpUnlocked = true;
        isGet = true;
        //Debug.Log(obj);
    }

    //    // Update is called once per frame
    //    void Update()
    //    {

    //    }
}




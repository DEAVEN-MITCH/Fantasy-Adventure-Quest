using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Restart_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Player pc;
    private Rigidbody2D rb;
    private Playercontroller pr;
    private void Awake() {
        Debug.Log("awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<Playercontroller>().inputControl;
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<Playercontroller>();
        pc.UI.Restart.started += RRestart;
        Debug.Log("start");
    }
    //private void OnEnable()
    //{
    //    //pc.Enable();
    //}
    //private void OnDisable()
    //{
    //    //pc.Disable();
    //}
    void RRestart(InputAction.CallbackContext obj)
    {
        rb.velocity = new Vector2();
        transform.position = new Vector3(-0.5112553f, 3, 0f);
        pr.isDead = false;
        pr.isHurt = false;
        pc.Gameplay.Enable();
        Character t = GetComponent<Character>();
        t.currentHealth = t.maxHealth;
    }
    // Update is called once per frame
    void Update()
    {

    }
}

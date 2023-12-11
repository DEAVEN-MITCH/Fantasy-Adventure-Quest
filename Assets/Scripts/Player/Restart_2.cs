using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Restart_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInputControl pc;
    private Rigidbody2D rb;
    private PlayerController pr;

    private SaveGameManager saveGameManager; // add

    private void Awake() {
        Debug.Log("awake");
        saveGameManager = new SaveGameManager(); // add
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>().inputControl;
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<PlayerController>();
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
        // ����浵����
        saveGameManager.ClearSaveData(); // add

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

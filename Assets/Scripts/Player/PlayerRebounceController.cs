using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerRebounceController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Character character;
    private PlayerController pc;
    private PlayerRebounceAnimation pra;
    private PlayerHealController phc;
    private PlayerDashController pdc;
    public int rebounceEnergyCost;
    public bool isRebounce;
    public GameObject rebounce;
    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        pra = GetComponent<PlayerRebounceAnimation>();
        character = GetComponent<Character>();
        phc = GetComponent<PlayerHealController>();
        pdc = GetComponent<PlayerDashController>();
        rebounce.SetActive(false);
    }

    //Start is called before the first frame update
    void Start()
    {
        inputControl = pc.inputControl;
        inputControl.Gameplay.Rebound.started += Rebound;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Rebound(InputAction.CallbackContext obj)
    {
        if (!phc.isHeal && !pc.isHurt && pc.currentPower >= rebounceEnergyCost&&!pdc.isDashing&&!isRebounce)
        {
            //Debug.Log("Rebounce Start");
            isRebounce = true;
        }
    }
}

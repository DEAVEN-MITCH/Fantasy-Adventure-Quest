using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerHealController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Character character;
    private PlayerController pc;
    private PlayerHealAnimation pha;

    public int healEnergyCost;
    public int healAmount;
    public bool isHeal;
    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        pha = GetComponent<PlayerHealAnimation>();
        character = GetComponent<Character>();
    }

    //Start is called before the first frame update
    void Start()
    {
        inputControl = pc.inputControl;
        inputControl.Gameplay.Heal.started += Heal;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Heal(InputAction.CallbackContext obj)
    {
        if (!isHeal && !pc.isHurt && pc.currentPower >= healEnergyCost && !character.HealthFull())
        {
            Debug.Log("Heal");
            isHeal = true;
            pha.PlayHeal();
        }
    }
}

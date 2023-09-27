using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(Attack attacker)
    {
        //Debug.Log(attacker.damage);
        if (invulnerable) return;
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke(); 
        }
    }

 

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        { invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }

    }
}

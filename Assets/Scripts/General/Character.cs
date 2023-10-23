using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;
    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public bool isDead=false;
    [Header("�¼�")]
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent OnDie;
    void Start()
    {
        currentHealth = maxHealth;
        if(transform.gameObject.name =="player")
        OnHealthChange?.Invoke(this);
    }
    public void TakeDamage(Attack attacker)
    {
        //Debug.Log(attacker.damage);
        if (invulnerable) return;
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //����
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else if(!isDead)
        {
            isDead = true;
            currentHealth = 0;
            OnDie?.Invoke();
            //Debug.Log("deadly Attack!");
        }
        OnHealthChange?.Invoke(this);
    }

    public void HealthRegen(int amount)
    {
        if (currentHealth + amount > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;
        OnHealthChange?.Invoke(this);
    }

    public bool HealthFull()
    {
        if (currentHealth >= maxHealth)
            return true;
        return false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Health Attributes")]
    public float maxHealth;
    public float currentHealth;
    [Header("Invulnerable Attributes")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;
    public bool isDead = false;
    [Header("Events")]
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent OnDie;
    [Header("Status")]
    public bool isFrost;
    public bool frostImmune;
    public float frostDuration;
    [Header("Speed")]
    public Vector2 speedBeforeCorrection;
    public Vector2 speedAfterCorrection;
    public float speedCorrection;
    public AudioSource getHurt;

    void Start()
    {
        currentHealth = maxHealth;
        if (transform.gameObject.name == "player")
            OnHealthChange?.Invoke(this);
        isFrost = false;
        frostDuration = 0;
    }
    public void TakeDamage(Attack attacker)
    {
        //Debug.Log(attacker.damage);
        if (invulnerable) return;
        if (transform.gameObject.name == "player")
            getHurt.Play();
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            // take damage
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else if (!isDead)
        {
            isDead = true;
            currentHealth = 0;
            OnDie?.Invoke();
            //Debug.Log("deadly Attack!");
        }
        OnHealthChange?.Invoke(this);
        //Apply abnormal status
        for (int i = 0; i < attacker.statusApply.Length; i++)
        {
            switch (attacker.statusApply[i])
            {
                case AbnormalStatus.Frost:
                    if (!frostImmune)
                        if (!isFrost || frostDuration < attacker.statusDuration[i])
                        {
                            isFrost = true;
                            frostDuration = attacker.statusDuration[i];
                        }
                    break;
                default:
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isFrost && frostDuration > 0)
        {
            frostDuration -= Time.deltaTime;
            if (frostDuration <= 0)
                isFrost = false;
        }

        //Correct Speed
        speedCorrection = 1.0f;
        if (isFrost)
            speedCorrection *= 0.5f;
    }

    public void HealthRegen(float amount)
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
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }

    }
}

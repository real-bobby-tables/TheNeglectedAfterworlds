using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityStats : MonoBehaviour
{
    public EntityAttributes attributes;
    
    private float currentHealth;
    private float currentDamage;
    private float currentDefense;
    private float currentSpeed;

    private Animator anim;
    private LifeSupport ls;

    public UnityEvent OnDeath;
    
    public void ResetStats()
    {
        currentHealth = attributes.Health;
        currentDamage = attributes.MeleeDamage;
        currentDefense = attributes.Defense;
        currentSpeed = attributes.Speed;
    }

    void Start()
    {
        ResetStats();
        anim = GetComponent<Animator>();
    }

    public void Damage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            OnDeath.Invoke();
        }
    }


    public void RegularDeath()
    {
        anim.SetBool("IsDead", true);
        Destroy(this.gameObject, 1);
        //display defeat menu here
        // then pause time
    }

     public void RestoreHP(float amount)
    {
        currentHealth += amount;
    }

    public void ModifyDamage(float amount)
    {
        currentDamage += amount;
    }

    public void ModifySpeed(float amount)
    {
        currentSpeed += amount;
    }

    public void ModifyDefense(float amount)
    {
        currentDefense += amount;
    }
    
}

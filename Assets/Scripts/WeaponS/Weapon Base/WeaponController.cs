using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]

    public WeaponObject weapon;
    float currentCooldown;
    

    protected PlayerController pc;
    protected virtual void Start()
    {
        currentCooldown = weapon.CooldownDuration;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weapon.CooldownDuration;
    }
}

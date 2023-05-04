using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;

    sfxController sfx;

    void Start()
    {
        sfx = FindObjectOfType<sfxController>();
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            sfx.PlaySFX(SFX.Die);
            Destroy(gameObject);
        }
    }

    private void OnCollisionTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack"))
        {
            float currentDamage = 0f;
            MeleeWeaponBehaviour meleeWeapon = col.GetComponent<MeleeWeaponBehaviour>();
            if (meleeWeapon != null)
            {
                currentDamage = meleeWeapon.GetCurrentDamage();
                TakeDamage(currentDamage);
            }
            else {
                ProjectileWeaponBehaviour projectileWeapon = col.GetComponent<ProjectileWeaponBehaviour>();
                if (projectileWeapon != null)
                {
                    currentDamage = projectileWeapon.GetCurrentDamage();
                    TakeDamage(currentDamage);
                }
                else {
                    Debug.LogError("Encountered weapon behaviour that isnt defined");
                }
            }
        }
    }
}

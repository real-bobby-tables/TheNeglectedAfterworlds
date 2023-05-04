using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponObject weaponData;
    public float destroyAfterSeconds;

   protected float currentDamage;
   protected float currentSpeed;
   protected float currentCooldownDuration;
   protected float currentPierce;

   private PlayerStats player;


   private void Awake()
   {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
        player = FindObjectOfType<PlayerStats>();
   }

    public float GetCurrentDamage()
    {
        return currentDamage *= player.currentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats en = col.GetComponent<EnemyStats>();
            en.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }

        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        --currentPierce;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

}

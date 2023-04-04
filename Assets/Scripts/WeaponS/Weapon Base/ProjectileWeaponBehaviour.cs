using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
   public WeaponObject weaponData;
   protected Vector3 direction;
   public float destroyAfterSeconds;

   protected float currentDamage;
   protected float currentSpeed;
   protected float currentCooldownDuration;
   protected float currentPierce;

   private void Awake()
   {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
   }

   public void DirectionChecker(Vector3 dir)
   {
    direction = dir;
   }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats en = col.GetComponent<EnemyStats>();
            en.TakeDamage(currentDamage);
            ReducePierce();
        }
        
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
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

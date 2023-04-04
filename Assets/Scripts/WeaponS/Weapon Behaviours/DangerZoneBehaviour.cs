using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;
    protected override void Start()
    {
        markedEnemies = new List<GameObject>();
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats en = col.GetComponent<EnemyStats>();
            en.TakeDamage(currentDamage);
            markedEnemies.Add(col.gameObject);
        }

        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);

                markedEnemies.Add(col.gameObject);
            }
        }
    }

}

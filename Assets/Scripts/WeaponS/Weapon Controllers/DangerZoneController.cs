using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDangerZone = Instantiate(weapon.Prefab);
        spawnedDangerZone.transform.position = transform.position;
        spawnedDangerZone.transform.parent = transform;
    }

    
}

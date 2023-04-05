using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : ProjectileWeaponBehaviour
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
        
    }
}

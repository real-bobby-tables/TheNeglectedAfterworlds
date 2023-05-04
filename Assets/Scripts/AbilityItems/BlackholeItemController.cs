using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeItemController : AbilityItem
{
    public float pullForce = 10f;
    public float mass = 10f;

    protected override void Init()
    {
        float rndForce = Random.Range(10, 300);
        float rndMass = Random.Range(5, 35); 

        pullForce = rndForce;
        mass = rndMass; 
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (didAbilityActivate)
        {
            Rigidbody2D colRb = col.GetComponent<Rigidbody2D>();
            if (colRb != null && col.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                float forceMagnitude = pullForce * mass * colRb.mass / Mathf.Pow(distance, 2f);

                Vector2 force = (transform.position - col.transform.position).normalized * forceMagnitude;
                colRb.AddForce(force);
            }
        }

    }

}

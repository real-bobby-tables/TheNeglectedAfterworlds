using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionItemController : AbilityItem
{
    public float repulsiveForce = 100f;
    public float repulsionRange = 5f;

    protected override void Init()
    {
        abilityName = "REPULSION";
        float rndForce = Random.Range(75, 500);
        float rndRange = Random.Range(5, 20);

        repulsiveForce = rndForce;
        repulsionRange = rndRange;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (didAbilityActivate)
        {
            Rigidbody2D colRb = col.GetComponent<Rigidbody2D>();
            if (colRb != null && col.CompareTag("Enemy"))
            {
                Vector2 repulsionDirection = (transform.position - col.transform.position).normalized;
                float distanceToItem = Vector2.Distance(transform.position, col.transform.position);
                float repulsionFactor = Mathf.Clamp01((repulsionRange - distanceToItem) / repulsionRange);
                colRb.AddForce(-repulsionDirection * repulsiveForce * repulsionFactor);
            }            
        }

    }
}

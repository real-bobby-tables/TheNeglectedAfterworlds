using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeAttack : MonoBehaviour
{
    public WeaponStats stats;
    public string TargetTag;
    
    private Rigidbody2D rb;
   

    private Vector2 direction = Vector2.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(direction != Vector2.zero)
        {
            rb.velocity = direction * stats.Speed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we only care about box colliders since those are attached to moving entities
        if (other.collider.GetType() == typeof(BoxCollider2D))
        {
            if (other.gameObject.tag == TargetTag)
            {
                EntityStats es = other.gameObject.GetComponent<EntityStats>();
                es.Damage(stats.Damage);
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }
}

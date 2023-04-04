using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{
   public int healthToRestore;

    public void Collect()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();
        ps.RestoreHealth(healthToRestore);
        Destroy(gameObject);
    }
}

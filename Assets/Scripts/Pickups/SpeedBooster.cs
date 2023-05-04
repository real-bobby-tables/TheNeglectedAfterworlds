using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : Pickup, ICollectible
{
    public float speedMultiplier;
    public float duration;

    public void Collect()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();
        Debug.Log("Attempting to modify speed stat");
        ps.ModifyStat(PlayerStats.AffectedStat.Speed, speedMultiplier, duration);
    }
}

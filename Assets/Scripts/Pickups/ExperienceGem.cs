using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();
        ps.IncreaseExperience(experienceGranted);
        Destroy(this.gameObject);
    }
}

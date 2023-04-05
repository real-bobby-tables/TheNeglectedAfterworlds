using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops 
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    PlayerStats stats;

    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
    }
    public List<Drops> drops;


    //TODO: improve this so that drops dont spawn when restarting the level
    void OnDestroy()
    {
        if (stats != null && !stats.IsDead())
        {
            float rnd = Random.Range(0f, 100f);
            List<Drops> possibleDrops = new List<Drops>();
            foreach(Drops rate in drops)
            {
                if (rnd <= rate.dropRate)
                {
                    possibleDrops.Add(rate);
                }
            }

            if (possibleDrops.Count > 0)
            {
                Drops drop = possibleDrops[Random.Range(0, possibleDrops.Count)];
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
            }
        }
        
    }
}

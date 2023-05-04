using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstone : MonoBehaviour
{

    sfxController sfx;
    public float damage = 10f;
    // Start is called before the first frame update

    private void Awake()
    {
        sfx = FindObjectOfType<sfxController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = col.gameObject.GetComponent<PlayerStats>();
            ps.TakeDamage(damage);
            if (sfx != null)
            {
                sfx.PlaySFX(SFX.PlayerHurt);
            }
        }
    }
}

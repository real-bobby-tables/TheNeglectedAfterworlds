using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    bool ShouldDestroy = false;
    float EffectDuration = 3.0f;

    public float RestoredHealth = 10.0f;

    public float SpeedMod = 5.0f;

    public float DamageMod = 5.0f;

    public float DefenseMod = 5.0f;

    public enum PowerupType {None, Repel, BackAtEm, Speedboost};
    public enum ItemType {None, Healing, Defense, Damage};
    public PowerupType PType = PowerupType.None;
    public ItemType IType = ItemType.None;
    private EntityStats playerStats;
    private PlayerController pc;
    private EnemyManager EM;
    public ItemEffect effect;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerStats = playerObj.GetComponent<EntityStats>();
        pc = playerObj.GetComponent<PlayerController>();
        EM = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldDestroy)
        {
            Destroy(this.gameObject);
        }
    }


    public IEnumerator EffectRoutine() 
    {
        switch (PType)
            {
                case PowerupType.None: {
                    yield return new WaitForSeconds(0.1f);
                    ShouldDestroy = true;
                } break;
                case PowerupType.Repel: {
                    pc.ActivateDefensePerimeter();
                    yield return new WaitForSeconds(EffectDuration);
                    pc.DeactivateDefensePerimeter();
                    ShouldDestroy = true;
                } break;
                case PowerupType.BackAtEm: {
                    GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectiles");
                    foreach(GameObject p in projectiles)
                    { 
                        var randy = EM.GetRandomEnemy();
                        p.GetComponent<BasicProjectile>().SetTarget(randy);
                    }
                    yield return new WaitForSeconds(1.0f);
                    ShouldDestroy = true;
                } break;
                case PowerupType.Speedboost: {
                    yield return new WaitForSeconds(EffectDuration);
                    ShouldDestroy = true;
                } break;
            }
    }

    public void ActivatePowerup()
    {
        StartCoroutine(EffectRoutine());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch(IType)
            {
                case ItemType.None: {} break;
                case ItemType.Healing: {
                    playerStats.RestoreHP(RestoredHealth);
                } break;
                case ItemType.Defense: {
                    playerStats.ModifyDefense(DefenseMod);
                } break;
                case ItemType.Damage: {
                    playerStats.ModifyDamage(DamageMod);
                } break;
            }
        }
    }
}

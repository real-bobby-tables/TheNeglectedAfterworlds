using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EntityAttributes enemyData;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despwanDistance = 20f;
    Transform player;

    EnemySpawner es;

    private void Awake()
    {
        currentMoveSpeed = enemyData.Speed;
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        es = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despwanDistance)
        {
            ReturnEnemy();
        }
    }

    void ReturnEnemy()
    {

    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died");
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = col.gameObject.GetComponent<PlayerStats>();
            ps.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        es.OnEnemyKilled();
    }
}

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

    public float despwanDistance = 40f;
    Transform player;

    EnemySpawner es;
    bool isDead;

    [HideInInspector]
    public bool canTryToRez = false;
    [HideInInspector]
    public bool isPlayerAlly = false;
    bool canArise = false;

    public bool Arise {get => canArise; set => value = canArise;}

    bool isWithinPlayerRange = false;

    bool foundEnemyTarget = false;

    public bool FoundTarget {get => foundEnemyTarget; set => foundEnemyTarget = value; }

    GameObject target = null;

    SpriteRenderer renderer;

    bool isInPlayerRange = false;
    bool respawn = false;

    public bool hasRezzed = false;



    public bool DidRespawn()
    {
        return respawn;
    }

    public bool IsInPlayerRange()
    {
        return isInPlayerRange;
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AllyZone"))
        {
            isInPlayerRange = true; //es.SetTarget(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("AllyZone"))
        {
            isInPlayerRange = false;
        }
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public GameObject GetTarget()
    {
        return target;
    }


    public void SetRezSuccessful(bool s)
    {
        es.OnEnemyKilled(); //do this so it wont count a rez'd enemy as a part of the wave 
        isPlayerAlly = s;
        gameObject.tag = "Mon";
        renderer.enabled = false;
        hasRezzed = s;
    }

    public void Respawn()
    {
        renderer.enabled = true;
        renderer.color = Color.cyan;
        transform.GetChild(0).gameObject.SetActive(true);
        respawn = true;
        Debug.Log("Finished respawning");
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Awake()
    {
        float difficulty = PlayerPrefs.GetFloat("Difficulty", 1f);
        currentMoveSpeed = enemyData.Speed * difficulty;
        currentDamage = enemyData.Damage * difficulty;
        currentHealth = enemyData.MaxHealth * difficulty;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //FindObjectOfType<PlayerStats>().transform;
        es = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();//FindObjectOfType<EnemySpawner>();
        isDead = false;
    }

    void Update()
    {
        if (!isDead)
        {
            if (Vector2.Distance(transform.position, player.position) >= despwanDistance)
            {
                ReturnEnemy();
            }
        }
        
        
    }

    void ReturnEnemy()
    {
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            isDead = true;
            if (isPlayerAlly)
            {
                die();
            }
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (!isPlayerAlly)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                PlayerStats ps = col.gameObject.GetComponent<PlayerStats>();
                ps.TakeDamage(currentDamage);
            }

            if (col.gameObject.CompareTag("Mon"))
            {
                EnemyStats es = col.gameObject.GetComponent<EnemyStats>();
                es.TakeDamage(currentDamage);
            }
        }

        else {
            if (col.gameObject.CompareTag("Enemy") && gameObject.tag != "Enemy")
            {
                EnemyStats es = col.gameObject.GetComponent<EnemyStats>();
                es.TakeDamage(currentDamage);
                if (es.IsDead())
                {
                    target = null;
                }
            }
        }

    }
    private void OnDestroy()
    {
        if (es != null)
        {
            es.OnEnemyKilled();
        }
        
    }
}

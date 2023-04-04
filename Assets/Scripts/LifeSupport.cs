using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LifeSupport : MonoBehaviour
{
    //private EntityStats stats;
    private float currentHealth;

    private Animator anim;
    private SpriteRenderer sprite;

    private EnemyManager em;
    private BoxCollider2D col;
    private PlayerController pc;
    private Vector3 respawnLocation = Vector3.zero;

    //add states for the sm for when the enemy respawns



    public void Reset()
    {
        //stats.ResetStats();
    }

    private bool CanBecomeMon()
    {
        float chance = Random.Range(0.0f, 1.0f);
        return pc.EnemyRezChance() > chance;
    }

    //modify the state machine to track and attack other enemies instead of the player
    private void ChangeBehavior()
    {
        
    }

    public void die()
    {
        //temporarily disable collision
        col.enabled = false;

        //determine if the enemy can instead be added to the players collection
        if (CanBecomeMon())
        {
            StartCoroutine(FakeDeath());
        }
        else {
            RealDeath();
        }
    }

    public void MonSpawn()
    {
        //maybe play some sort of animation here?

        //re-enable collision
        col.enabled = true;
        //then unhide
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator FakeDeath()
    {
        //play the death enimation
        respawnLocation = this.gameObject.transform.position;
        anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(1.2f);
        GetComponent<SpriteRenderer>().enabled = false;
        Reset(); //reset the stats of this enemy
        ChangeBehavior(); //then change it's state machine a little bit to target other enemies instead of players
        //then add it to the list of enemy mons
        em.RemoveEnemy(this.gameObject);
        em.AddMon(this.gameObject);

        //might not wanna use that? not sure
        this.gameObject.SetActive(false);
    }

    private void RealDeath()
    {
        col.enabled = false;
        anim.SetBool("IsDead", true);
        em.RemoveEnemy(this.gameObject);
        Destroy(this.gameObject, 1);
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        em = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        //stats = GetComponent<EntityStats>();
    }
}

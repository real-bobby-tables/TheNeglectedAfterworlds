using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float BaseSpeed = 5.0f;
    private float currentSpeed;
    private float currentDamage;
    public float currentDefense;
    private float currentHealth;

    private EnemyManager em;


    private CircleCollider2D defensePerimeter;

    public EntityAttributes attributes;
    private Animator anim;
    private SpriteRenderer sprite;
    public GameObject attackProjectile;

    public PlayerInput playerControls;

    private InputAction move;
    private InputAction dash;
    private InputAction fire;
    private InputAction useItem;
    private InputAction usePowerup;
    public float TimeUntilFiring = 1.0f;
    private float fireWaitTime = 0.0f;
    
    private Vector2 moveDirection = Vector2.zero;
    
    public float dashAmount = 330.0f;

    public float ChanceToRes = 0.2f;

    private MainGameUI mui = null;
    public GameObject MainUIGameObject;
    



    #region stuff
   

    public float EnemyRezChance()
    {
        return ChanceToRes;
    }

    public void Damage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            anim.SetBool("IsDead", true);
            Debug.Log("Player died");
            Destroy(this.gameObject, 2);
            SceneManager.LoadScene(2);
        }
    }

    public void ActivateDefensePerimeter()
    {
        defensePerimeter.enabled = true;
    }

    public void DeactivateDefensePerimeter()
    {
        defensePerimeter.enabled = false;
    }

    private void Awake()
    {
        currentSpeed = BaseSpeed;
        //currentSpeed = attributes.Speed;
        currentHealth = attributes.Health;
        currentDamage = attributes.MeleeDamage;
        currentDefense = attributes.Defense;
        playerControls = new PlayerInput();
        //i dunno if this should be moved to start or not
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        defensePerimeter = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
        fire.Disable();
    }

    IEnumerator DoDash(Vector2 dir)
    {
        //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        rb.AddForce(dir * dashAmount);//, ForceMode2D.Impulse);
        Debug.Log("WE DASHED");
        yield return new WaitForSeconds(0.4f);
    }

    IEnumerator FunRotationMovement()
    {
        currentSpeed += Random.Range(0.0f, 1.0f) * (Random.Range(0, 10) % 2 == 0 ? 1 : -1);
        yield return new WaitForSeconds(3.0f);
        transform.rotation = Quaternion.identity;
        currentSpeed = BaseSpeed;
    }

    #endregion
    void Start()
    {
        em = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        if (MainUIGameObject != null)
        {
            mui = MainUIGameObject.GetComponent<MainGameUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        UpdateAnimation(moveDirection);

        if (transform.rotation.z != 0)
        {
            StartCoroutine(FunRotationMovement());
        }

        fireWaitTime += Time.deltaTime;
        if (fireWaitTime >= TimeUntilFiring)
        {
            if (em.HasEnemies())
            {
                Vector3 spawnPos = transform.position + ((new Vector3(moveDirection.x, moveDirection.y, 0) * 5));
                if (attackProjectile != null)
                {
                    var en = em.GetRandomEnemy();
                    if (en != null)
                    {
                        GameObject projectile = Instantiate(attackProjectile, spawnPos, Quaternion.identity);
                        //projectile.transform.localScale = new Vector3(3, 3, 0);
                        PlayerProjectile p = projectile.GetComponent<PlayerProjectile>();
                        if (p != null)
                        {
                            p.SetTarget(en);
                        }
                    }
                }
            }
            fireWaitTime = 0.0f;
        }
    }

    private void UpdateAnimation(Vector2 dir)
    {
        if (dir.x != 0 || dir.y != 0)
        {
            anim.SetBool("IsRunning", true);
            if (dir.x < 0)
            {
                sprite.flipX = true;
            }
            else {
                sprite.flipX = false;
            }
        }
        else {
            anim.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        if (dash.WasPerformedThisFrame())
        {
            StartCoroutine(DoDash(moveDirection));
        }
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);

    }
}

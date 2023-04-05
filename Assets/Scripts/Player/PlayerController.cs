using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float currentSpeed;
    private float currentDamage;
    private float currentDefense;
    private float currentHealth;

    private EnemyManager em;


    private CircleCollider2D defensePerimeter;
    private Animator anim;
    private SpriteRenderer sprite;
    [Header("Input")]
    PlayerInput playerControls;

    private InputAction move;
    private InputAction dash;
    private InputAction fire;
    private InputAction useItem;
    private InputAction usePowerup;
    private float fireWaitTime = 0.0f;
    
    private Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDir {
        get {return moveDirection;}
    }

    public Vector2 LastMoveDir {
        get {return new Vector2(LastHorizontalDir, LastVerticalDir);}
    }
    
    //public float dashAmount = 330.0f;

    public float ChanceToRes = 0.2f;

    PlayerStats stats;

    [HideInInspector]
    private float LastHorizontalDir;
    private float LastVerticalDir;
    private Rigidbody2D rb;

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
            Destroy(this.gameObject, 2);
            SceneManager.LoadScene(2);
        }
    }

    void Awake()
    {
        Debug.Log("In awake");
        playerControls = new PlayerInput();
        if (playerControls == null)
        {
            Debug.Log("Input is null for some reason");
        }
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        
        dash = playerControls.Player.Dash;
        dash.Enable();
    }

    void OnDisable()
    {
        Debug.Log("In disable");
        if (move != null)
        {
            Debug.Log("Disabling movement");
            move.Disable();
        }
        else {
            Debug.Log("Failed to disable movement, it was null");
        }
        dash.Disable();
    }

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        currentDefense = 0f;
        currentSpeed = stats.currentMoveSpeed;
        currentHealth = stats.currentHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        LastHorizontalDir = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        if (moveDirection.x != 0)
        {
            LastHorizontalDir = moveDirection.x;
        }

        if (moveDirection.y != 0)
        {
            LastVerticalDir = moveDirection.y;
        }
        UpdateAnimation(moveDirection);

    }

    private void UpdateAnimation(Vector2 dir)
    {
        if (dir.x != 0 || dir.y != 0)
        {
            anim.SetBool("IsRunning", true);
            if (LastHorizontalDir < 0)
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
        /*
        if (dash.WasPerformedThisFrame())
        {
            Debug.Log("attempted to dash");
        }
        */
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);

    }

}

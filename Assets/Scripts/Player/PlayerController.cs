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
    public PlayerInput playerControls;

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

    private MainGameUI mui = null;
    PlayerStats stats;
    public GameObject MainUIGameObject;

    [HideInInspector]
    private float LastHorizontalDir;
    private float LastVerticalDir;
    private Rigidbody2D rb;
    



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
        currentSpeed = stats.currentMoveSpeed;
        currentHealth = stats.currentHealth;
        //currentDamage = stats.Damage;
        currentDefense = 0f;
        playerControls = new PlayerInput();
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



    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (MainUIGameObject != null)
        {
            mui = MainUIGameObject.GetComponent<MainGameUI>();
        }

        LastHorizontalDir = 1.0f;
        stats = GetComponent<PlayerStats>();
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
        if (dash.WasPerformedThisFrame())
        {
            Debug.Log("attempted to dash");
        }
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);

    }

}

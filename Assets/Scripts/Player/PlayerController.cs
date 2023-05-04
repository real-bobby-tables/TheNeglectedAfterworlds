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

    private CircleCollider2D defensePerimeter;
    private Animator anim;
    private SpriteRenderer sprite;
    [Header("Input")]
    PlayerInput playerControls;

    private InputAction move;
    private InputAction soulRelease;
    private InputAction useAbility;
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

    private InventoryManager inventory;
    bool didUlt = false;

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

    public void DisableMovement()
    {
        move.Disable();
    }

    public void EnableMovement()
    {
        move.Enable();
    }

    void Awake()
    {
    
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
        
        soulRelease = playerControls.Player.SoulRelease;
        soulRelease.Enable();

        useAbility = playerControls.Player.UseAbility;
        useAbility.Enable();
    }

    void OnDisable()
    {
        
        if (move != null)
        {
            Debug.Log("Disabling movement");
            move.Disable();
        }
        else {
            Debug.Log("Failed to disable movement, it was null");
        }
        soulRelease.Disable();
        useAbility.Disable();
    }

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        inventory = stats.GetInventory();
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
        if (!stats.IsDead())
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

            if (soulRelease.IsPressed())
            {
                Debug.Log("Attempted to ult");
                if (stats.CanUlt())
                {
                    Debug.Log("Used Soul Release");
                    didUlt = true;
                    StartCoroutine(ResetUltWindow());
                }
            }    

            if (useAbility.IsPressed() && !inventory.IsUsingAbility())
            {
                if (inventory.HasAbilityItem())
                {
                    inventory.UseAbility();
                }
            }        
        }
    }

    IEnumerator ResetUltWindow()
    {
        yield return new WaitForSeconds(4f);
        didUlt = false;
    }

    public bool DidUlt()
    {
        return didUlt;
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
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);
    }

}

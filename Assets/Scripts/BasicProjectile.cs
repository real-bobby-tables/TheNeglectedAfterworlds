using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public WeaponStats stats;
    private Rigidbody2D rb;
    
    public bool Homing;
    private GameObject Target;

    public string[] TargetTags;

    private float speed;
    public float timeToLive = 2.5f;
    private float timeLived = 0.0f;
    private Vector2 movePosition = Vector2.zero;
    private Vector2 dir = Vector2.zero;



    public void SetTarget(GameObject T)
    {
        Target = T;
    }

    private void Awake()
    {
        this.gameObject.tag = "Projectile";
        speed = stats.Speed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 d)
    {
        dir = d;
    }

    public void SetHoming(bool homing)
    {
        Homing = homing;
    }

    // Update is called once per frame
    void Update()
    {
        //maybe i can remove this?
        timeLived += Time.deltaTime;
        if (timeLived > timeToLive)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        
        float moveSpeed = Time.fixedDeltaTime * speed;
        if (Homing && Target != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, Target.transform.position, moveSpeed);
            rb.MovePosition(newPosition);
        }
        else {
            rb.velocity = dir * speed;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetType() == typeof(BoxCollider2D))
        {

            for(int i = 0; i < TargetTags.Length; i++)
            {
                if (other.gameObject.tag == TargetTags[i])
                {
                    EntityStats es = other.gameObject.GetComponent<EntityStats>();
                    if (es != null)
                    {
                        es.Damage(stats.Damage);
                    }
                    Destroy(this.gameObject);
                }
            }
        }

        if (other.collider.GetType() == typeof(CircleCollider2D))
        {
            Debug.Log("Hit the forcefield");
            //maybe do something to actually stop this? or not
        }
        
        Debug.Log("we hit something, name: " + other.gameObject.name);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("we're still hitting something");
        if (TargetTags.Length > 0)
        {
            if (other.gameObject.tag == TargetTags[0])
            {
                Debug.Log("Deleting projectile");
                Destroy(this.gameObject);
            }
        }
        
    }
}

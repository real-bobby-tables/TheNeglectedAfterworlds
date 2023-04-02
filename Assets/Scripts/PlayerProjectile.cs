using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public WeaponStats stats;
    private Rigidbody2D rb;

    private GameObject target;
    public float speed = 30.0f;

    public float timeToLive = 6.5f;
    private float timeLived = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 3));
        timeLived += Time.deltaTime;
        if (timeLived > timeToLive)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        
        float moveSpeed = Time.fixedDeltaTime * speed;
        if (target != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
            rb.MovePosition(newPosition);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetType() == typeof(BoxCollider2D))
        {
            if (other.gameObject.tag == "Enemy")
            {
                EntityStats es = other.gameObject.GetComponent<EntityStats>();
                es.Damage(stats.Damage);
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("we're still hitting something in the player projectile");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Deleting projectile");
            Destroy(this.gameObject);
        }
    }

}

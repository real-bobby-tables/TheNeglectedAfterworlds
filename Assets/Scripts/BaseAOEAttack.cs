using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAOEAttack : MonoBehaviour
{
    
    public WeaponStats stats;
    public string[] TargetTags;

    public float timeToLive = 3.5f;
    private float timeLived = 0.0f;

    void Start()
    {
        
    }

   
    void Update()
    {
        timeLived += Time.deltaTime;
        if (timeLived > timeToLive)
        {
            Destroy(this.gameObject);
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
                }
            }
        }
    }

    
    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("we're still hitting something");
        if (TargetTags.Length > 0)
        {
            if (other.gameObject.tag == TargetTags[0])
            {
                EntityStats es = other.gameObject.GetComponent<EntityStats>();
                if (es != null)
                {
                    es.Damage(stats.Damage);
                }
            }
        }
        
    }
}

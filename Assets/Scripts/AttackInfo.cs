using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    public enum ProjectileAttackInfo {
        OneDirectionProjectile,
        HomingProjectile,
    }

    private Vector2 last_known_dir = Vector2.zero;
    private GameObject target = null;
    
    public bool TargetPlayer = true;

    public Vector2 LastKnownDir {
        get {return last_known_dir; }
        set {last_known_dir = value; }
    }

    public GameObject Target {
        get {return target;}
        set {target = value; }
    }
    public ProjectileAttackInfo projectileInfo; 


    //should try to make sure that this only applies to the circle collider
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetType() == typeof(BoxCollider2D))
        {
            if (other.gameObject.tag != "Player")
            {
                target = other.gameObject;
            }
        }
    }

}

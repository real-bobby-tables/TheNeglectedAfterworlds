using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
   
   private CircleCollider2D searchArea;

    void Start()
    {
        searchArea = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        searchArea.radius *= 3;
    }

   
    void Update()
    {
        
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}

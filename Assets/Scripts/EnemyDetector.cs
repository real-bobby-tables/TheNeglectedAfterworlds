using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    EnemyStats es;
    // Start is called before the first frame update
    void Start()
    {
        es = gameObject.GetComponentInParent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            es.SetTarget(col.gameObject);
        }
    }

}

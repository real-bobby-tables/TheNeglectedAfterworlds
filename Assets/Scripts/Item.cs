using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedItem : MonoBehaviour
{

    public string name;
    public float duration = 1.0f;
    bool canDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canDestroy)
        {
            Destroy(gameObject);
        }
    }

   protected virtual void ActualEffect()
   {

   }

    IEnumerator ItemEffect()
    {
        ActualEffect();
        yield return new WaitForSeconds(duration);
        canDestroy = true;
        
    }

    void StartEffect()
    {
        StartCoroutine(ItemEffect());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartEffect();
        }
    }
}

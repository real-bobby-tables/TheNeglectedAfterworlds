using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    public string abilityName = "";
    public float duration;
    float timeElapsed = 0f;
    protected bool didAbilityActivate = false;

    protected InventoryManager inv;
    bool didPickup;

    protected virtual void Init()
    {

    }
    public virtual void ActivateAbility()
    {
        didAbilityActivate = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !didPickup)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            didPickup = true;
            inv.AddAbility(this);
            gameObject.transform.parent = inv.gameObject.transform;
        }
    }
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        didPickup = false;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (didAbilityActivate)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= duration)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        inv.SetUsingAbility(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/AttackAction")]
public class AttackAction : FSMAction
{
    public GameObject AttackObj;
    public GameObject AOEAttackObj;
    public GameObject AttackProjectile;
    public float attackCooldown = 2.0f;
    private bool didAttack = false;
    //private float TimeTillAttack = 1.0f;

    /*
    private  IEnumerator Fire(BaseStateMachine stateMachine)
    {
        GameObject projectile = Instantiate(AttackProjectile, stateMachine.gameObject.transform.position + Vector3.down, Quaternion.identity);
        var bp = projectile.GetComponent<BasicProjectile>();
        bp.enabled = true;
        AttackInfo info = stateMachine.GetComponent<AttackInfo>();
        switch(info.projectileInfo)
        {
            case AttackInfo.ProjectileAttackInfo.OneDirectionProjectile: {
                bp.SetDirection(info.LastKnownDir);
            } break;
            case AttackInfo.ProjectileAttackInfo.HomingProjectile: {
                bp.SetHoming(true);
                if (info.TargetPlayer)
                {
                    bp.SetTarget(stateMachine.GetPlayer());
                }
                else if (info.Target != null)
                {
                    bp.SetTarget(info.Target);
                }

                else {
                    bp.SetTarget(stateMachine.gameObject);
                }
                
            } break;
        }
        
        didAttack = true;
        yield return new WaitForSeconds(attackCooldown);
        didAttack = false;
    }

    private  IEnumerator CastAOE(BaseStateMachine stateMachine)
    {
        didAttack = true;
        //get the players position, then add like 30 to it to create two points, i.e. a radius 
        //then from that radius, spawn an AOE entity somewhere in that circle
        //https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly
        Vector3 aoe_pos = new Vector3(0, 0, 0);
        GameObject aoe_spell = Instantiate(AttackObj, aoe_pos, Quaternion.identity);
        aoe_spell.AddComponent<BaseAOEAttack>();
        yield return new WaitForSeconds(attackCooldown);
        didAttack = false;
        
    }

    private IEnumerator MeleeAttack(BaseStateMachine stateMachine)
    {
        BaseMeleeAttack ma = stateMachine.GetComponent<BaseMeleeAttack>();
        ma.enabled = true;
        AttackInfo info = stateMachine.GetComponent<AttackInfo>();
        ma.SetDirection(info.LastKnownDir);
        didAttack = true;
        yield return new WaitForSeconds(attackCooldown);
        didAttack = false;
    }
    */
    public override void Execute(BaseStateMachine stateMachine)
    {
       if (AOEAttackObj != null)
       {
            if (!didAttack)
            {
                
                //stateMachine.StartCoroutine(CastAOE(stateMachine));
            }
       }

       if (AttackProjectile != null)
       {
            if(!AOEAttackObj)
            {
                //stateMachine.StartCoroutine(Fire(stateMachine));
            }
       }

        if (AttackObj != null)
        {
           if (!didAttack)
            {
                //stateMachine.StartCoroutine(MeleeAttack(stateMachine));
            }
        }
    }


}

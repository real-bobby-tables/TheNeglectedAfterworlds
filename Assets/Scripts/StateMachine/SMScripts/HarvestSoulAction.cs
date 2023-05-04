using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Actions/HarvestSoulAction")]
public class HarvestSoulAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
       EnemyStats es = stateMachine.GetComponent<EnemyStats>();
        if (!es.hasRezzed)
        {
            PlayerStats ps = stateMachine.GetPlayer().GetComponent<PlayerStats>();
            ps.GetInventory().AddSoul(stateMachine.gameObject);
            stateMachine.GetComponent<Animator>().SetBool("IsDead", false);
            es.SetRezSuccessful(true);
            Debug.Log("Harvest successful");
        }
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
       
    }
}

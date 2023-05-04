using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Actions/SoulReleaseAction")]
public class SoulReleaseAction : FSMAction
{
    
    public override void Execute(BaseStateMachine stateMachine)
    {
        Debug.Log("In soul release action");
        PlayerStats ps = stateMachine.GetPlayer().GetComponent<PlayerStats>();
        ps.GetInventory().RemoveSoul(stateMachine.gameObject);
        Debug.Log("Removed soul from collection, now calling respawn");
        stateMachine.GetComponent<EnemyStats>().Respawn();
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
       // throw new System.NotImplementedException();
    }
}

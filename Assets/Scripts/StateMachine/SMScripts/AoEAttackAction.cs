using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/AOEAttack")]
public class AoEAttackAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        //var enemy = stateMachine.GetComponent<Enemy>();
        //enemy.SpawnAOEEntity();
    }
}

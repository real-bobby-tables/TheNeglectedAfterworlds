using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/ConduitAction")]
public class ConduitAction : FSMAction
{
   
    public override void Execute(BaseStateMachine stateMachine)
    {
        Debug.Log("In conduit, should transition to other state soon");
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
        
    }
}
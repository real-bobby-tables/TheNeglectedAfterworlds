using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/ActuallyDieAction")]
public class ActuallyDieAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        Debug.Log("Harvest failed, destroying enemy object now");
        Destroy(stateMachine.gameObject);
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
       // throw new System.NotImplementedException();
    }
}

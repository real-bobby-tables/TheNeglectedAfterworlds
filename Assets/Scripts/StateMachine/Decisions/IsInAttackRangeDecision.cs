using TNA.FSM;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Decisions/IsInAttackRange")]
public class IsInAttackRangeDecision : Decision
{
    public float MaxDistance = 10.0f;
    public override bool Decide(BaseStateMachine state)
    {
        return true;
        //var info = state.GetComponent<AttackInfo>();
        //might have to flip operands
        //info.LastKnownDir = state.gameObject.transform.position - state.GetPlayer().transform.position;
        //return Vector3.Distance(state.gameObject.transform.position, state.GetPlayer().transform.position) < MaxDistance;
    }
}



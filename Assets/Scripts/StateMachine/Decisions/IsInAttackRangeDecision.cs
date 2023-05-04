using TNA.FSM;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Decisions/IsInAttackRange")]
public class IsInAttackRangeDecision : Decision
{
    public float MaxDistance = 10.0f;
    public override bool Decide(BaseStateMachine state)
    {
       var enemy = state.gameObject;
       var player = state.GetPlayer();
       return Vector3.Distance(enemy.transform.position, player.transform.position) < MaxDistance;
    }
}



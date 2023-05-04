using TNA.FSM;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Decisions/IsNowDying")]

public class IsNowDying : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        return state.GetComponent<EnemyStats>().IsDead();
    }
}

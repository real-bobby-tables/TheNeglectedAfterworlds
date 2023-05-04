using TNA.FSM;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Decisions/CanFindPlayer")]
public class CanFindPlayer : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        
        return state.GetComponent<EnemyStats>().DidRespawn() == true;
    }
}
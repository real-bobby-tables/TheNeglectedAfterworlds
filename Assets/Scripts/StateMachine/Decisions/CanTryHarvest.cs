using TNA.FSM;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Decisions/CanTryHarvest")]
public class CanTryHarvest : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        
        return state.GetComponent<EnemyStats>().canTryToRez == true;
    }
}
using TNA.FSM;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Decisions/CanHarvestSoul")]
public class CanHarvestSoul : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        
        return state.GetPlayer().GetComponent<PlayerStats>().CanRezEnemy() == true;
    }
}
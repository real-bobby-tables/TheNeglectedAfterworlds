using TNA.FSM;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Decisions/CanRespawn")]
public class CanRespawn : Decision
{

    public override bool Decide(BaseStateMachine state)
    {
        return state.GetPlayer().GetComponent<PlayerController>().DidUlt() == true;
    }
}
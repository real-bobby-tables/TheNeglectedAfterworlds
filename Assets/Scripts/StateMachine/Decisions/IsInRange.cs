using TNA.FSM;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/IsInRange")]
public class IsInRange : Decision
{
    public float MaxDistance = 10.0f;
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var enemy = stateMachine.gameObject;
        var player = stateMachine.GetPlayer();

        return Vector3.Distance(enemy.transform.position, player.transform.position) < MaxDistance;
    }
}

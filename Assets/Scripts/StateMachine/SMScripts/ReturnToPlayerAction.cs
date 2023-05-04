using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/ReturnToPlayerAction")]
public class ReturnToPlayerAction : FSMAction
{
    public float MaxDistance = 10f;
    public override void Execute(BaseStateMachine stateMachine)
    {
        var self = stateMachine.gameObject;

        var target = stateMachine.GetPlayer();
        //float moveSpeed = self.GetComponent<EnemyStats>().currentMoveSpeed * Time.deltaTime;
        if (Vector3.Distance(self.transform.position, target.transform.position) > 20f)
        {
            Vector3 newPosition = Vector3.MoveTowards(self.transform.position, target.transform.position, MaxDistance);
            bool shouldFlip = newPosition.x < 0;
            self.GetComponent<SpriteRenderer>().flipX = shouldFlip;
            self.transform.position = newPosition;
        }

        //Debug.Log("Returning to player...");
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }
}

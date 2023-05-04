using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/AttackAction")]
public class AttackAction : FSMAction
{
   
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.GetComponent<Animator>().SetBool("IsAttacking", true);
        EnemyStats es = stateMachine.GetComponent<EnemyStats>();
        GameObject player = stateMachine.GetPlayer();
        GameObject self = stateMachine.gameObject;
        self.transform.position = Vector2.MoveTowards(self.transform.position, player.transform.position, es.currentMoveSpeed * Time.deltaTime);
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
        
    }
}

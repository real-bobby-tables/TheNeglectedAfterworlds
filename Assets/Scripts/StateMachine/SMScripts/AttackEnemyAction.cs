using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Actions/AttackEnemyAction")]

public class AttackEnemyAction : FSMAction
{
    
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.GetComponent<Animator>().SetBool("IsAttacking", true);
        EnemyStats es = stateMachine.GetComponent<EnemyStats>();
        GameObject target = es.GetTarget();
        if (target != null)
        {
            GameObject self = stateMachine.gameObject;
            self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, es.currentMoveSpeed * Time.deltaTime);            
        }
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
        
    }
}

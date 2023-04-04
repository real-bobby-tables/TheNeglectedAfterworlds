using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Actions/EnemyChaseAction")]
public class EnemyChaseAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        /*
        var enemy = stateMachine.gameObject;

        var target = stateMachine.GetPlayer();
        float moveSpeed = enemy.GetComponent<EntityStats>().attributes.Speed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(enemy.transform.position, target.transform.position, moveSpeed);
        bool shouldFlip = newPosition.x < 0;
        enemy.GetComponent<SpriteRenderer>().flipX = shouldFlip;
        enemy.transform.position = newPosition;
        */
    }
}

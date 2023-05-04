using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Actions/DyingAction")]
public class DyingAction : FSMAction
{
    float duration = 2.0f;
    float t = 0;
    bool didSetAnim = false;
    Animator anim;
    public override void Execute(BaseStateMachine stateMachine)
    {
        if (!didSetAnim)
        {
            anim = stateMachine.GetComponent<Animator>();
            anim.SetBool("IsDead", true);
            sfxController sfx = FindObjectOfType<sfxController>();
            sfx.PlaySFX(SFX.Die);
            didSetAnim = true;
            Debug.Log("Enemy died");
        }  
        if(t < duration)
        {
            t += Time.deltaTime;
        }
        else {
            stateMachine.GetComponent<SpriteRenderer>().enabled = false;
            //stateMachine.GetComponent<Animator>()
            stateMachine.GetComponent<EnemyStats>().canTryToRez = true;
            Debug.Log("Attempting to rez enemy....");
        }
    }

    public override void FixedExecute(BaseStateMachine stateMachine)
    {
       // throw new System.NotImplementedException();
    }
}

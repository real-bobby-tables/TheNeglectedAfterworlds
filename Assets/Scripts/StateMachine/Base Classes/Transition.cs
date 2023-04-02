//using TNA.FSM;
using UnityEngine;

namespace TNA.FSM {
    
    [CreateAssetMenu(menuName = "FSM/Transition")]
    public sealed class Transition : ScriptableObject
    {

        public Decision decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if (decision.Decide(stateMachine) && !(TrueState is RemainInState))
            {
                stateMachine.CurrentState = TrueState;
            }
            else if (!(FalseState is RemainInState))
            {
                stateMachine.CurrentState = FalseState;
            }
        }
    }
}


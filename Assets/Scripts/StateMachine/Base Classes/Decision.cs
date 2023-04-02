using UnityEngine;



namespace TNA.FSM {
        public abstract class Decision : ScriptableObject
        {
            public abstract bool Decide(BaseStateMachine state);
        }
}


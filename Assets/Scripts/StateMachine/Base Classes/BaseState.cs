using UnityEngine;

public class BaseState : ScriptableObject
{

    public virtual void Execute(BaseStateMachine machine) {
        
    }

    public virtual void FixedExecute(BaseStateMachine machine)
    {
        
    }
}

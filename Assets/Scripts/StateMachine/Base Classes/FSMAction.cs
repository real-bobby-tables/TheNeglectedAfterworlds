using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public abstract void Execute(BaseStateMachine stateMachine);

    public abstract void FixedExecute(BaseStateMachine stateMachine);
}

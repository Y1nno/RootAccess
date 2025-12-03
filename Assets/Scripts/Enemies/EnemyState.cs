using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [Tooltip("Unique ID used to switch to this state, e.g. 'Patrol', 'Chase', 'Attack'")]
    public string stateId = "Unnamed";

    public abstract void Enter(EnemyStateMachine machine);
    public abstract void Tick(EnemyStateMachine machine, float deltaTime);
    public abstract void Exit(EnemyStateMachine machine);
}

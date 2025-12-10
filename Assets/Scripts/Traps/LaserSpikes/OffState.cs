using UnityEngine;

public class OffState : EnemyState
{
    public EnemyState onState;
    public float offStateLength = 2;
    public float timeInitialOffset = 0;

    private float internalTimer = 0;

    public void Start()
    {
        internalTimer = offStateLength + timeInitialOffset;
    }

    public override void Enter(EnemyStateMachine machine)
    {
        internalTimer += offStateLength;
        //Debug.Log("Off");
    }

    public override void Tick(EnemyStateMachine machine)
    {
        internalTimer -= Time.deltaTime;
        if (internalTimer <= 0)
        {
            machine.ChangeState(onState.stateId);
        }
    }

    public override void Exit(EnemyStateMachine machine)
    {

    }
}

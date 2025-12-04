using UnityEngine;

public class WoodieIdleState : EnemyState
{
    public float endAfter = 1f;
    public string PatrolStateID = "Patrol";

    private float internalTimer = 0f;




    public override void Enter(EnemyStateMachine machine)
    {

    }

    public override void Tick(EnemyStateMachine machine)
    {
        internalTimer += Time.deltaTime;
        if (internalTimer >= endAfter)
        {
            internalTimer = 0;
            machine.ChangeState(PatrolStateID);

        }
    }

    public override void Exit(EnemyStateMachine machine)
    {

    }
}

using UnityEngine;

//[CreateAssetMenu(menuName = "Scripts/Enemies/Woodie/Patrol")]
public class WoodiePatrolState : EnemyState
{
    public float pauseEvery = 1f;
    public string IdleStateID = "Idle";

    private float internalTimer = 0f;

    [HideInInspector] public PatrollingEnemyController patrolController;




    public override void Enter(EnemyStateMachine machine)
    {
        patrolController = machine.GetComponent<PatrollingEnemyController>();
        if (patrolController != null)
           patrolController.enabled = true;
    }

    public override void Tick(EnemyStateMachine machine, float deltaTime)
    {
        internalTimer += deltaTime;
        if (internalTimer >= pauseEvery)
        {
            internalTimer = 0;
            machine.ChangeState(IdleStateID);

        }
    }

    public override void Exit(EnemyStateMachine machine)
    {
        if (patrolController != null)
            patrolController.enabled = false;
    }
}

using UnityEngine;

public class OnState : EnemyState
{
    public EnemyState offState;
    public Animator animator;

    public float onStateLength = 10f;
    
    private float internalLengthTimer = 0;

    public override void Enter(EnemyStateMachine machine)
    {
        animator.SetTrigger("Activate");
        internalLengthTimer = onStateLength;
        //Debug.Log("On");
    }

    public override void Tick(EnemyStateMachine machine)
    {
        if (internalLengthTimer <= 0f)
        {
            machine.ChangeState(offState.stateId);
            return;
        }
        internalLengthTimer -= Time.deltaTime;
    }

    public override void Exit(EnemyStateMachine machine)
    {
        animator.SetTrigger("Deactivate");
    }
}

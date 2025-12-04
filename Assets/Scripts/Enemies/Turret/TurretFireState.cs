using UnityEngine;

public class TurretFireState : EnemyState
{
    public EnemyState trackingState;
    public Transform turretTop;
    public Animator animator;

    public bool temp;

    public override void Enter(EnemyStateMachine machine)
    {
        animator = turretTop.GetComponent<Animator>();
        animator.SetTrigger("PreFire");
        
    }

    public override void Tick(EnemyStateMachine machine)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            turretTop.GetComponent<FireProjectile>().Fire();
            turretTop.GetComponent<Animator>().SetTrigger("Fire");
            machine.ChangeState(trackingState.stateId);
        }
        
    }

    public override void Exit(EnemyStateMachine machine)
    {
        
    }
}
using UnityEngine;

public class TurretIdleState : EnemyState
{
    public Transform turretTop;
    public EnemyState trackingState;

    // Use signed angles now
    public float rotationUpperBound = 45f;   // +angle
    public float rotationLowerBound = -45f;  // -angle

    private float turnDirection = 1f;

    public float waitTimer = 2f;
    private float timer = 0f;
    private bool isWaiting = false;

    public float turnRate = 30f;

    public float temp;

    public override void Enter(EnemyStateMachine machine)
    {
        if (!turretTop)
        {
            turretTop = machine.transform.GetChild(0);
        }

        timer = 0f;
        isWaiting = false;
    }

    public override void Tick(EnemyStateMachine machine)
    {
        if (!turretTop) return;

        float angle = Mathf.DeltaAngle(0f, turretTop.localRotation.eulerAngles.z);

        temp = angle;

        if (isWaiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                
                isWaiting = false;
            }
            return;
        }

        //Hit upper bound
        if (angle >= rotationUpperBound - 0.5f)
        {
            turretTop.Rotate(0, 0, turnRate * turnDirection * Time.deltaTime);
            StartWait();
            turnDirection = -1f;
            turretTop.Rotate(0, 0, turnDirection);
            return;
        }

        //Hit lower bound
        if (angle <= rotationLowerBound + 0.5f)
        {
            turretTop.Rotate(0, 0, turnRate * turnDirection * Time.deltaTime);
            StartWait();
            turnDirection = 1f;
            turretTop.Rotate(0, 0, turnDirection);
            return;
        }

        //Sweep normally
        turretTop.Rotate(0, 0, turnRate * turnDirection * Time.deltaTime);
    }

    private void StartWait()
    {
        isWaiting = true;
        timer = waitTimer;
    }

    public override void Exit(EnemyStateMachine machine)
    {
    }
}

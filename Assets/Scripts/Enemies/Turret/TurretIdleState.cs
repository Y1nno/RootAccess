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

    [Header("Gizmo")]
    public float gizmoRadius = 30f;
    public int gizmoSegments = 24;

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

        // Get signed local angle (-180 .. 180)
        float angle = Mathf.DeltaAngle(0f, turretTop.localRotation.eulerAngles.z);

        // If the turret's current rotation is outside the configured bounds,
        // snap it to the lower bound and reset sweep state.
        if (angle < rotationLowerBound || angle > rotationUpperBound)
        {
            turretTop.localRotation = Quaternion.Euler(0f, 0f, rotationLowerBound);
            angle = rotationLowerBound;
            temp = angle;

            // reset state so turret starts sweeping upward from the lower bound
            isWaiting = false;
            timer = 0f;
            turnDirection = 1f;
            return;
        }

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

    // Draw the allowed sweep arc and current direction.
    // If a related FireProjectile has flipFireDirection == true the gizmo is vertically flipped (mirrored across the local Y axis).
    private void OnDrawGizmos()
    {
        bool flipGizmo = false;

        if (!turretTop) return;
        var fp = turretTop.GetComponent<FireProjectile>();
        if (fp != null)
        {
            flipGizmo = fp.flipFireDirection;
        }

        // Fallback: if not found directly on turretTop, search children.
        if (!flipGizmo)
        {
            var fpLocal = turretTop.GetComponentInChildren<FireProjectile>();
            if (fpLocal != null)
                flipGizmo = fpLocal.flipFireDirection;
        }

        // Reference transform for local->world mapping (parent if present, otherwise turretTop)
        Transform reference = turretTop.parent ? turretTop.parent : turretTop;

        Vector3 origin = turretTop.position;
        float radius = Mathf.Max(0.01f, gizmoRadius);
        int segments = Mathf.Max(4, gizmoSegments);

        // Helper: compute world direction for a local angle, applying a vertical (Y-axis) mirror in local space when requested.
        Vector3 GetWorldDir(float angle)
        {
            Vector3 localDir = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
            if (flipGizmo)
            {
                // Mirror across local Y axis -> negate local X
                localDir.x = -localDir.x;
            }
            return reference.TransformDirection(localDir.normalized);
        }

        float startAngle = rotationLowerBound;
        float endAngle = rotationUpperBound;
        float span = endAngle - startAngle;
        float step = span / segments;

        Gizmos.color = Color.cyan;

        Vector3 prevPoint = origin + GetWorldDir(startAngle) * radius;
        for (int i = 1; i <= segments; i++)
        {
            float a = startAngle + step * i;
            Vector3 dir = GetWorldDir(a);
            Vector3 nextPoint = origin + dir * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }

        // Draw bounds lines
        Gizmos.color = Color.white;
        Vector3 startDir = GetWorldDir(startAngle);
        Vector3 endDir = GetWorldDir(endAngle);
        Gizmos.DrawLine(origin, origin + startDir * radius);
        Gizmos.DrawLine(origin, origin + endDir * radius);

        // Draw current turret direction (mirrored in local space if flipGizmo)
        float currentLocalAngle = Mathf.DeltaAngle(0f, turretTop.localRotation.eulerAngles.z);
        Vector3 currentDir = GetWorldDir(currentLocalAngle);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + currentDir * (radius * 1.1f));
    }
}
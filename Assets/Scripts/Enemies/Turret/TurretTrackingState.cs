using UnityEngine;
using System;

public class TurretTrackingState : EnemyState
{
    public Transform turretTop;
    public EnemyState idleState;
    [HideInInspector] public Transform player;

    public float turnRate = 10f;

    private float angleToPlayer = 0f;  

    public override void Enter(EnemyStateMachine machine)
    {
        player = machine.player;
        //Debug.Log("State Entered: Tracking");
    }

    public override void Tick(EnemyStateMachine machine)
    {
        if (!turretTop || !player) return;

        Transform muzzle = turretTop.GetChild(1); 
        Transform tip = turretTop.GetChild(0);

        Vector2 barrelDir = (tip.position - muzzle.position);
        Vector2 toPlayerDir = (player.position - muzzle.position);

        angleToPlayer = Vector2.Angle(barrelDir, toPlayerDir);

        float signedAngle = Vector2.SignedAngle(barrelDir, toPlayerDir) * gameObject.transform.lossyScale.x;

        if (Mathf.Abs(signedAngle) > 1f)
        {
            float maxStep = turnRate * Time.deltaTime;
            float step = Mathf.Clamp(signedAngle, -maxStep, maxStep);
            turretTop.Rotate(0f, 0f, step);
        }
    }

    private void OnDrawGizmos()
    {
        if (!turretTop || !player) return;

        Transform muzzle = turretTop.GetChild(0);
        Transform tip = turretTop.GetChild(1);

        Vector3 origin = muzzle.position;
        Vector3 barrelDir3 = (tip.position - muzzle.position).normalized;
        Vector3 toPlayerDir3 = (player.position - muzzle.position).normalized;

        float rayLength = 20f;
        float arcRadius = 10f;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, barrelDir3 * rayLength);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, toPlayerDir3 * rayLength);

        Gizmos.color = Color.cyan;
        DrawAngleArc(origin, barrelDir3, toPlayerDir3, arcRadius, 24);
    }

    private void DrawAngleArc(Vector3 center, Vector3 dirA, Vector3 dirB, float radius, int segments)
    {
        Vector2 a = new Vector2(dirA.x, dirA.y).normalized;
        Vector2 b = new Vector2(dirB.x, dirB.y).normalized;

        float angle = Vector2.SignedAngle(a, b);
        float step = angle / segments;

        Vector3 prevPoint = center + (Vector3)a * radius;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = step * i;
            Vector2 rotated = Rotate2D(a, currentAngle);
            Vector3 nextPoint = center + (Vector3)rotated * radius;

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

    private Vector2 Rotate2D(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    public override void Exit(EnemyStateMachine machine)
    {
    }
}

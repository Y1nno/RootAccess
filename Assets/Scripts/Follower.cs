using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Tooltip("Whether or not we should actually follow")]
    public bool allowFollowing = true;

    public Transform target;

    [Tooltip("How much we should dampen or ease our movement when following")]
    public float damping = 0.5f;

    [Tooltip("Don't edit! This just shows our current velocity")]
    public Vector3 velocity;

    [Tooltip("Don't edit! This just shows our current offset from the followed object")]
    public Vector3 offset;




    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = transform.parent;

        if (target != null)
            offset = transform.position - target.position;
    }

    // FixedUpdate is called 25 times per second. This is used for physics. We're moving the camera here because the
    // player moves every FixedUpdate
    private void FixedUpdate()
    {
        if (target == null || !allowFollowing) return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            damping,
            Mathf.Infinity,
            Time.fixedDeltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a mover to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Mover))]
public class PatrollingEnemyController : MonoBehaviour 
{
    [Tooltip("How far ahead the enemy looks to try to find the ground")]
    public float looksAheadDistance = 0.5f;
    [Tooltip("How far down the enemy looks to try to find the ground")]
    public float looksDownwardDistance = 1f;
    [Tooltip("Layermask for what counts as the ground for the enemy")]
    public LayerMask raycastLayermask;

    //Reference Variables
    private SpriteRenderer spriteRenderer;
    private Mover controlledMover;

    //Which direction we're facing (-1 = left, 1 = right)
    private float movementDirection;

    //Raycasts want a var to save the hit. This doesn't get used besides that
    private RaycastHit hit;

	// Use this for initialization
	void Start () 
    {
        //Set reference vars
        spriteRenderer = GetComponent<SpriteRenderer>();
        controlledMover = GetComponent<Mover>();

        //Start enemy facing left
        movementDirection = -1.0f;
	}

    //Draw gizmos is 100% a debug function. Players WILL NOT see it. It is only to help us design
    public void OnDrawGizmos()
    {
        // Use a default direction so gizmos don't break in edit mode when Start hasn't run yet
        float gizmoDirection = movementDirection;
        if (gizmoDirection == 0f)
            gizmoDirection = -1f;

        // Calculate where the raycast should start
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.x += looksAheadDistance * gizmoDirection;

        // Direction & length of the ray (downwards)
        Vector3 raycastDirection = Vector3.down * looksDownwardDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOrigin, raycastDirection);
    }

    // Update is called once per frame
    void Update () 
    {
        //Calculate the ray origin
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.x += looksAheadDistance * movementDirection;

        //Cast a ray and see if it hits something
        if (Physics2D.Raycast(raycastOrigin, -Vector3.up, looksDownwardDistance, raycastLayermask))
        {
            //If it does hit something, accelerate in the currently set direction
            controlledMover.AccelerateInDirection(new Vector3(movementDirection, 0f, 0f));
        }
        else
        {
            //If it doesn't hit anything, we know a cliff is coming up, so turn around
            movementDirection *= -1f;

            //If we have a sprite renderer, turn that around too
            if(spriteRenderer != null)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
	}
}

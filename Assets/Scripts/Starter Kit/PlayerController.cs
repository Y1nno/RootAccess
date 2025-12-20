using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a mover and a jumper to work. This will automatically add them if there isn't one of each
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]

public class PlayerController : MonoBehaviour
{
    //these are all just references to the various components attached to this object to make our lives easier. We'll add more as we go!
    private Mover mover;
    private Jumper jumper;
    private FireProjectile fireProjectile;
    private PlayerCharacterSwapper swapper;
    private Animator animator;
    private Rigidbody2D myRigidbody;
    [HideInInspector]
    public bool dialogueActive = false;

    public GameObject dialogueManager;


    void Start()
    {
        //Find all the componenets attached to this object and save them to references
        mover = gameObject.GetComponent<Mover>();
        jumper = gameObject.GetComponent<Jumper>();
        fireProjectile = gameObject.GetComponent<FireProjectile>();
        swapper = GetComponentInParent<PlayerCharacterSwapper>();
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Running", false);

        if (!jumper.GetIsOnGround() && myRigidbody.linearVelocity.y <= 0 && !animator.GetBool("Falling"))
        {
            animator.SetBool("Falling", true);
        }

        if (animator.GetBool("Falling") && jumper.GetIsOnGround())
        {
            animator.SetBool("Falling", false);
            animator.SetTrigger("Land");
        }

        if (Input.GetKeyUp(KeyCode.Space) && dialogueActive == true)
        {
            dialogueManager.GetComponent<SpriteSpeakerIndex>().ContinueDialogue();
        }

        if (dialogueActive) { return; }

        //Moving Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Running", true);
            }

            //When right key is pressed, accelerate towards the right...
            mover.AccelerateInDirection(new Vector2(1f, 0f));

            //And flip our entire body to face the right
            var e = transform.eulerAngles;
            e.y = 0f;
            transform.eulerAngles = e;
        }

        //Moving Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Running", true);
            }

            //When left key is pressed, accelerate towards the left...
            mover.AccelerateInDirection(new Vector2(-1f, 0f));

            //And flip our entire body to face the left
            var e = transform.eulerAngles;
            e.y = 180f;
            transform.eulerAngles = e;
        }

        //When Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //If the jump key is pressed... jump!
            jumper.Jump();

        }
        if (swapper.currentIndex == 1) // Olivia specific inputs
        {
            if (Input.GetKey(KeyCode.E) && fireProjectile.cooldownTimer <= 0f)
            {
                fireProjectile.Fire();
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [Tooltip("Number indicating how much force we jump with")]
    public float jumpImpulse = 5f;

    [Tooltip("Horizontal impulse applied when jumping off a wall (world X based on local right)")]
    public float wallJumpImpulse = 5f;

    [Tooltip("Whether or not the player currently has Double Jump unlocked")]
    public bool doubleJumpAllowed = false;

    public float coyoteTime = 0.2f;

    [HideInInspector]
    public float timeLastOnGround;

    public float jumpBufferTime = 0.05f;
    private float timeJumpPressed = -999f;

    public bool leftGroundByJumping = false;

    [Tooltip("What sound to play when we jump if we have an Audio Source")]
    public AudioClip jumpSound;

    //A boolean that detects whether or not we are touching something. Allowing us to jump again.
    private bool isOnGround;
    private bool isTouchingWall;
    private bool hasDoubleJumped;

    //Reference variable to attached Rigidbody2D
    private Rigidbody2D myRigidbody;
    private Animator animator;

    private PlayerCharacterSwapper swapper;

    // Start is called before the first frame update
    void Start()
    {
        //Store attached Rigidbody2D
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        swapper = GetComponentInParent<PlayerCharacterSwapper>();
    }

    //Function that gets called whenever we need to jump
    public void Jump()
    {
        // 1) Ground jump
        if (isOnGround)
        {
            leftGroundByJumping = true;
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpImpulse);
        }
        // 2) Wall jump (typically when in air and touching wall)
        else if (isTouchingWall && CanCoyoteJump())
        {
            leftGroundByJumping = true;
            hasDoubleJumped = false; //Reset double jump on wall jump
            Vector3 away = -transform.right;
            float horiz = away.x * wallJumpImpulse;
            myRigidbody.linearVelocity = new Vector2(horiz, jumpImpulse);
        }
        // 3) Coyote jump (recently left ground)
        else if (CanCoyoteJump())
        {
            leftGroundByJumping = true;
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpImpulse);
        }
        // 4) Double jump (if allowed and not used yet)
        else if (doubleJumpAllowed && !hasDoubleJumped && swapper != null && swapper.currentIndex == 0)
        {
            hasDoubleJumped = true;
            leftGroundByJumping = true;
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpImpulse);
        }
        // 5) Otherwise, buffer the jump input
        else
        {
            timeJumpPressed = Time.time;
            return;
        }

        //If we have an audio source and a jump sound, play it
        AudioSource src = GetComponent<AudioSource>();
        if (src != null && jumpSound != null)
        {
            src.PlayOneShot(jumpSound);
        }

        animator.SetTrigger("Jump");
    }

    public bool CanCoyoteJump()
    {
        return (Time.time - timeLastOnGround) <= coyoteTime && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Ascend") || animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
    }

    public bool GetIsOnGround()
    {
        return isOnGround;
    }

    public void setCollider(colliderFace face, bool state)
    {
        if (face == colliderFace.BOTTOM)
        {
            isOnGround = state;
            if (state == true)
            {
                hasDoubleJumped = false;
                leftGroundByJumping = false;
                timeLastOnGround = Time.time;

                if ((Time.time - timeJumpPressed) <= jumpBufferTime)
                {
                    Jump();
                    timeJumpPressed = -jumpBufferTime;
                }
            }
        }
        else if (face == colliderFace.FRONT)
        {
            isTouchingWall = state;
        }
    }

    public void ChangeDoubleJump(bool? newBool)
    {
        if (newBool != null)
        {
            doubleJumpAllowed = newBool.Value;
        }
        else
        {
            doubleJumpAllowed = !doubleJumpAllowed;
        }
    }
}
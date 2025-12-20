using UnityEngine;

public class WallGrabHelper : MonoBehaviour
{
    public FrontColliderHelper frontColliderHelper;
    public BottomColliderHelper bottomColliderHelper;
    public Animator animator;
    public Jumper jumper;

    // Update is called once per frame
    void Update()
    {
        if (frontColliderHelper.colliderActive && !bottomColliderHelper.colliderActive)
        {
            animator.SetBool("WallGrab", true);
            jumper.timeLastOnGround = Time.time;
        }
        else
        {
            animator.SetBool("WallGrab", false);
        }
    }
}

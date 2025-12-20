using UnityEngine;

public class BottomColliderHelper : MonoBehaviour
{
    public Jumper jmpr;

    public bool colliderActive = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms&Walls"))
        {
            jmpr.setCollider(colliderFace.BOTTOM, true);
            colliderActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms&Walls"))
        {
            jmpr.setCollider(colliderFace.BOTTOM, false);
            colliderActive = false;
        }
    }
}

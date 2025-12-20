using UnityEngine;

public enum colliderFace
{
    FRONT,
    BACK,
    BOTTOM
}

public class FrontColliderHelper : MonoBehaviour
{
    public Jumper jmpr;

    public bool colliderActive = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms&Walls"))
        {
            jmpr.setCollider(colliderFace.FRONT, true);
            colliderActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms&Walls"))
        {
            jmpr.setCollider(colliderFace.FRONT, false);
            colliderActive = false;
        }
    }
}

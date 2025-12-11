using UnityEngine;

public class LoreCollectable : MonoBehaviour
{
    public GameObject textObject;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            textObject.SetActive(true);
        }
        else
        {
            textObject.SetActive(false);
        }
    }

}

using UnityEngine;

public class Warper : MonoBehaviour
{
    private GameObject destination;

    void Start()
    {
        destination = transform.parent.GetChild(1).gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (destination)
        {
            collision.gameObject.transform.position = destination.transform.position;
            return;
        }
        Debug.Log("Warper can't find Warp Destination");
    }

}

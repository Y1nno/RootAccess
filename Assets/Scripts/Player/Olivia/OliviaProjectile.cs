using UnityEngine;

public class OliviaProjectile : MonoBehaviour
{
    public float lifetime = 5f;

    // Update is called once per frame
    void Update()
    {
        // reduce the timer every frame
        if (lifetime > 0)
            lifetime -= Time.deltaTime;
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

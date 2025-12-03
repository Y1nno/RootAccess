using UnityEngine;

public class KickHitBox : MonoBehaviour
{
    public float lifetime = 0.25f;
    public float maxXScaleMultiplier = 2f;
    public bool dontDestroy = false;

    private float timer = 0f;
    private Vector3 initialScale;
    private float initialX;
    private float targetX;

    void Start()
    {
        initialScale = transform.localScale;
        initialX = initialScale.x;
        targetX = initialX * maxXScaleMultiplier;

        if (!dontDestroy)
        {
            Destroy(gameObject, lifetime);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / lifetime);

        float newX = Mathf.Lerp(initialX, targetX, t);

        transform.localScale = new Vector3(newX, initialScale.y, initialScale.z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}

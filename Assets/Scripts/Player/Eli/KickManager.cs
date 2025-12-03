using UnityEngine;

public class KickManager : MonoBehaviour
{
    public float cooldown = 0.5f;
    public float cooldownTimer = 0f;

    public GameObject projectilePrefab;          

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (cooldownTimer > 0f)
            return;

        cooldownTimer = cooldown;

        GameObject newKick = Instantiate(
            projectilePrefab,
            transform.position,
            transform.rotation
        );
    }
}

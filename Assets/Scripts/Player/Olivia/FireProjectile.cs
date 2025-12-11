using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;  // where the projectile spawns



    public float projectileSpeed = 10f;
    public float minimumCountdown = 0.5f;  // time between shots
    public float cooldownTimer = 0f;
    public bool canShoot = false;

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (!canShoot) { return; }


        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * projectileSpeed;

        cooldownTimer = minimumCountdown;
    }

    public void ChangeShootingAbility(bool? newBool)
    {
        if (newBool != null)
        {
            canShoot = newBool.Value;
        }
        else
        {
            canShoot = !canShoot;
        }
    }
}

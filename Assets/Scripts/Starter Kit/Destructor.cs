using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [Tooltip("How much damage this should do to destructibles")]
    public int damage = 1;
    [Tooltip("Which faction this Destructor is. Destructors don't damage Destructibles of the same faction.")]
    public int faction = 1;
    [Tooltip("How hard anything this damages should be pushed back")]
    public float knockbackForce = 0f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();

        if (destructible && destructible.faction != faction)
        {
            destructible.TakeDamage(damage);

            Rigidbody2D rb = collision.rigidbody;

            if (rb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

                rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

}

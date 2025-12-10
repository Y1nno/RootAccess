using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpikeDestructor : MonoBehaviour
{
    [Tooltip("How much damage this should do to destructibles")]
    public int damage = 1;
    [Tooltip("Which faction this Destructor is. Destructors don't damage Destructibles of the same faction.")]
    public int faction = 1;
    [Tooltip("How hard anything this damages should be pushed back")]
    public float knockbackForce = 0f;

    public EnemyStateMachine machine;

    private OnState state;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Damage(collision);
    }

    private void Damage(Collider2D collision)
    {
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();

        if (destructible && destructible.faction != faction)
        {
            //Debug.Log("triggered");

            destructible.TakeDamage(damage);

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

                rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickup : MonoBehaviour
{
    [Tooltip("The sound to play on pickup, if any")]
    public AudioClip pickupSound;
    public bool playerOnly = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Contact");
        PlayerController playerController =
            collision.gameObject.GetComponent<PlayerController>();

        if (!playerOnly || playerController)
        {
            //Debug.Log("Triggering OnPickup");
            OnPickup(collision);
        }
    }

    public void OnPickup(Collider2D collision)
    {
        //If we have an audio source and a sound...
        if(GetComponent<AudioSource>() && pickupSound)
        {
            //Play the sound!
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
        }

        //Turn off our collider
        GetComponent<Collider2D>().enabled = false;

        //Turn off our sprite renderer
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        Invoke("Die", 5f);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}

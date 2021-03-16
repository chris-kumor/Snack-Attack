using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float incrementHealth;
    public AudioClip Healing;
    private AudioSource ItemAudioSource;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9)
        {
            ItemAudioSource.PlayOneShot(Healing, 0.2f);
            collision.collider.gameObject.GetComponent<PlayerController>().ChangeHealth("+", incrementHealth);
            Destroy(gameObject);
        }
    }
}

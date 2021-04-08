using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float incrementHealth;
    public AudioClip Healing;
    public AudioSource ItemAudioSource;

    void OnCollisionEnter2D(Collision2D collision)
    {
        ItemAudioSource.PlayOneShot(Healing, 0.2f);
        collision.gameObject.GetComponent<PlayerController>().ChangeHealth("+", incrementHealth);
        Destroy(gameObject);
    }
}

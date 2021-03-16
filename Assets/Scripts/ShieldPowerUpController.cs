using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpController : MonoBehaviour
{
    private AudioSource ShieldAudioSource;
    public  AudioClip itemPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        ShieldAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9)
        {
            ShieldAudioSource.PlayOneShot(itemPickedUp, 0.2f);
            collision.collider.gameObject.GetComponent<PlayerController>().shield.canFire = true;
            collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldownTimer = collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldown;
            Destroy(gameObject);
        }
    }
}

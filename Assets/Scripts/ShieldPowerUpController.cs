using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            collision.collider.gameObject.GetComponent<PlayerController>().shield.canFire = true;
            collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldownTimer = collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldown;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float incrementHealth;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9)
        {
            collision.collider.gameObject.GetComponent<PlayerController>().ChangeHealth("+", incrementHealth);
            Destroy(gameObject);
        }
    }
}

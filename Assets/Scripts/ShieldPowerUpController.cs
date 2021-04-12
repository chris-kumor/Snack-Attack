using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUpController : MonoBehaviour
{
    public GameObject MeleePlayer, RangedPlayer;
    private AtkStruct MShield, RShield;

    void Start()
    {
        MShield = MeleePlayer.gameObject.GetComponent<PlayerController>().shield;
        RShield = RangedPlayer.gameObject.GetComponent<PlayerController>().shield;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MShield.cooldownTimer == MShield.cooldown)
            Physics2D.IgnoreLayerCollision(9, 20, true);
        else if(MShield.cooldownTimer != MShield.cooldown)
            Physics2D.IgnoreLayerCollision(9, 20, false);
        if(RShield.cooldownTimer == RShield.cooldown)
            Physics2D.IgnoreLayerCollision(19, 20, true);
        else if(RShield.cooldownTimer != RShield.cooldown)
            Physics2D.IgnoreLayerCollision(19, 20, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9 || collision.collider.gameObject.layer == 19)
        {
            collision.collider.gameObject.GetComponent<PlayerController>().shield.canFire = true;
            collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldownTimer = collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldown;
            Destroy(gameObject);
        }
    }
}

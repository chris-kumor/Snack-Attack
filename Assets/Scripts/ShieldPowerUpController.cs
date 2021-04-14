using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUpController : MonoBehaviour
{
    private GameObject MeleePlayer, RangedPlayer;
    private PlayerController MController, RController;

    void Start()
    {
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        MController = MeleePlayer.gameObject.GetComponent<PlayerController>();
        RController = RangedPlayer.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MController.shield.cooldownTimer == MController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(9, 20, true);
        else if(MController.shield.cooldownTimer != MController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(9, 20, false);
        if(RController.shield.cooldownTimer == RController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(19, 20, true);
        else if(RController.shield.cooldownTimer != RController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(19, 20, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9 || collision.collider.gameObject.layer == 19)
        {
            PlayerController collidedPlayerController = collision.collider.gameObject.GetComponent<PlayerController>();
            collidedPlayerController.shield.canFire = true;
            collidedPlayerController.shield.cooldownTimer = collidedPlayerController.shield.cooldown;
            Destroy(gameObject);
        }
    }
}

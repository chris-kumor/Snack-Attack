using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUpController : MonoBehaviour
{
    private GameObject MeleePlayer, RangedPlayer, Map, MeleeShield, RangedShield;
    private PlayerController MController, RController;
    private SpawnItems itemSpawner;
    private ShieldController rShieldController, mShieldController;


    void Start()
    {
        Map = GameObject.FindWithTag("Map");
        MeleeShield = GameObject.FindWithTag("MeleeShield");
        RangedShield = GameObject.FindWithTag("RangedShield");
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        MController = MeleePlayer.gameObject.GetComponent<PlayerController>();
        RController = RangedPlayer.gameObject.GetComponent<PlayerController>();
        rShieldController = RangedShield.GetComponent<ShieldController>();
        mShieldController = MeleeShield.GetComponent<ShieldController>();
        itemSpawner = Map.GetComponent<SpawnItems>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MController.shield.cooldownTimer == MController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(20, 9, true);
        else if(MController.shield.cooldownTimer != MController.shield.cooldown && mShieldController.isExposed == 1)
            Physics2D.IgnoreLayerCollision(20, 9, false);
        if(RController.shield.cooldownTimer == RController.shield.cooldown)
            Physics2D.IgnoreLayerCollision(19, 9, true);
        else if(RController.shield.cooldownTimer != RController.shield.cooldown && rShieldController.isExposed == 1)
            Physics2D.IgnoreLayerCollision(19, 9, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if((collision.collider.gameObject.layer == 20 || collision.collider.gameObject.layer == 19))
        {
            PlayerController collidedPlayerController = collision.collider.gameObject.GetComponent<PlayerController>();
            if(collidedPlayerController != null)
            {
                collidedPlayerController.shield.canFire = true;
                collidedPlayerController.shield.cooldownTimer = collidedPlayerController.shield.cooldown;
                itemSpawner.items -= 1;
                Destroy(gameObject);
            }
        }
    }
}

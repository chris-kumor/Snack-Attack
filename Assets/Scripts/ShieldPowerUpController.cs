using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUpController : MonoBehaviour
{
    private PolygonCollider2D MeleePlayerCollider, RangedPlayerCollider;
    public GameObject MeleePlayer, RangedPlayer;

    // Start is called before the first frame update
    void Start()
    {

        MeleePlayerCollider = MeleePlayer.GetComponent<PolygonCollider2D>();
        RangedPlayerCollider = RangedPlayer.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MeleePlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() == MeleePlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(9, 20, true);
        }
        else if(MeleePlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() != MeleePlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(9, 20, false);
        }

        if(RangedPlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() == RangedPlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(19, 20, true);
        }
        else if(RangedPlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() != RangedPlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(19, 20, false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Shield PowerUp Collider colliding with a player's collider
        if(collision.collider.gameObject.layer == 9 && collision.gameObject.GetComponent<PlayerController>().shield.cooldownTimer != collision.gameObject.GetComponent<PlayerController>().shield.cooldown)
        {
            collision.collider.gameObject.GetComponent<PlayerController>().shield.canFire = true;
            collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldownTimer = collision.collider.gameObject.GetComponent<PlayerController>().shield.cooldown;
            Destroy(gameObject);
        }
    }
}

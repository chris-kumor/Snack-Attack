using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float incrementHealth;
    private PolygonCollider2D MeleePlayerCollider, RangedPlayerCollider;
    public GameObject MeleePlayer, RangedPlayer;
    
    void Start()
    {
        
        MeleePlayerCollider = MeleePlayer.GetComponent<PolygonCollider2D>();
        RangedPlayerCollider = RangedPlayer.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if(MeleePlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() == MeleePlayer.GetComponent<PlayerController>().MaxHP)
            Physics2D.IgnoreLayerCollision(9, 15, true);
    
        else if(MeleePlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() != MeleePlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(9, 15, false);
        }

        if(RangedPlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() == RangedPlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(19, 15, true);
        }
        else if(RangedPlayer.gameObject.GetComponent<PlayerController>().GetPlayerHP() != RangedPlayer.GetComponent<PlayerController>().MaxHP)
        {
            Physics2D.IgnoreLayerCollision(19, 15, false);
        }

    }
 


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameStats.isBattle && collision.gameObject.GetComponent<PlayerController>().GetPlayerHP() != collision.gameObject.GetComponent<PlayerController>().MaxHP)
        {
        collision.gameObject.GetComponent<PlayerController>().ChangeHealth("+", incrementHealth);
        Destroy(gameObject);
        }
    }
}

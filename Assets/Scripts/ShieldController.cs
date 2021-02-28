using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ShieldController : MonoBehaviour
{
    public AtkStruct shield;
    private Rigidbody2D rb;
    private SpriteRenderer shieldSprite;
    Color ShieldFullColor ;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        shieldSprite = gameObject.GetComponent<SpriteRenderer>();
        shield.cooldownTimer = shield.cooldown;
        shield.canFire = true;
        ShieldFullColor = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], 0.75f);
        shieldSprite.color = ShieldFullColor;
        
        
    }

    void Update()
    {
        if(Input.GetKey(shield.fireKey) && shield.canFire && shield.cooldownTimer > 0)
        {
            shieldSprite.enabled = true;
        }
        else
        {
            shieldSprite.enabled = false;
        }

    


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(shieldSprite.enabled == true && shield.cooldownTimer > 0)
        {
            shield.cooldownTimer -= Time.deltaTime;
            shieldSprite.color = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], shieldSprite.color[3] - (Time.deltaTime/10));
        }

        if(shield.cooldownTimer <= 0)
        {
            shield.canFire = false;
            shieldSprite.color = ShieldFullColor;
        }
    }

}

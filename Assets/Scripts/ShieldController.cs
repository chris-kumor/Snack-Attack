using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ShieldController : MonoBehaviour
{
    public AtkStruct shield;
    public int isExposed;

    private Rigidbody2D rb;
    private SpriteRenderer shieldSprite;
    private Color ShieldFullColor;
    private CircleCollider2D shieldCollider;
    private AudioSource ShieldAudioSource;
    private SinputSystems.InputDeviceSlot slot;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        shieldSprite = gameObject.GetComponent<SpriteRenderer>();
        shieldCollider = gameObject.GetComponent<CircleCollider2D>();
        shield.cooldownTimer = shield.cooldown;
        shield.canFire = true;
        ShieldFullColor = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], 0.75f);
        shieldSprite.color = ShieldFullColor;
        isExposed = 1;
        ShieldAudioSource = gameObject.GetComponent<AudioSource>();
        if(gameObject.tag == "MeleePlayer")
        {
            slot = GameStats.MeleeSlot;
        }
        else
        {
            slot = GameStats.RangedSlot;
        }
        
        
    }

    void Update()
    {
        if(Sinput.GetButton(shield.fireKey, slot) && shield.canFire && shield.cooldownTimer > 0)
        {
            shieldSprite.enabled = true;
            shieldCollider.enabled = true;
            isExposed = 0;
            ShieldAudioSource.PlayOneShot(shield.soundToPlay, 0.05f);
        }
        else
        {
            shieldSprite.enabled = false;
            shieldCollider.enabled = false;
             isExposed = 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ShieldController : MonoBehaviour
{
    public AtkStruct shield;
    public int isExposed;
    public SpriteRenderer shieldSprite;
    public CircleCollider2D shieldCollider;
    public AudioSource ShieldAudioSource;
    public GameObject Player;


    private SinputSystems.InputDeviceSlot slot;
    private Color ShieldFullColor = new Color(1.00f, 0.0f, 0.0f, 0.75f);
    private PlayerController playerController;

    public string shieldName; 
    private bool isMShield;

    public void PlayerShielding(string shieldFireKey)
    {
        if(Sinput.GetButton(shieldFireKey, slot) && shield.canFire && shield.cooldownTimer > 0 && !playerController.attackStatus() && !playerController.isDashing)
        {
            shieldSprite.enabled = true;
            shieldCollider.enabled = true;
            isExposed = 0;
            if(slot == GameStats.MeleeSlot)
                Physics2D.IgnoreLayerCollision(9, 16, true);
            else if(slot == GameStats.RangedSlot)
                Physics2D.IgnoreLayerCollision(19, 16, true);
            ShieldAudioSource.PlayOneShot(shield.soundToPlay, 0.05f);
        }
        else
        {
            shieldSprite.enabled = false;
            shieldCollider.enabled = false;
            if(slot == GameStats.MeleeSlot)
                Physics2D.IgnoreLayerCollision(9, 16, false);
            else if(slot == GameStats.RangedSlot)
                Physics2D.IgnoreLayerCollision(19, 16, false);
             isExposed = 1;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        isMShield = (gameObject.tag == "MeleeShield");
        shieldName = shield.fireKey;
        shield.cooldownTimer = shield.cooldown;
        shield.canFire = true;
        shieldSprite.color = ShieldFullColor;
        isExposed = 1;
        ShieldAudioSource = gameObject.GetComponent<AudioSource>();
        playerController = Player.GetComponent<PlayerController>();
        if(isMShield)
            slot = GameStats.MeleeSlot;
        else
            slot = GameStats.RangedSlot;

        if(GameStats.bothPlayersKB)
            shieldName = shield.altFireKey;


    }

    void Update()
    {

    


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerShielding(shieldName);
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

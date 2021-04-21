using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldController : MonoBehaviour
{
    public float shieldHP, MaxHP;
    public SpriteRenderer shieldSprite;
    private Color shieldFullColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    public CircleCollider2D bossCollider, shieldCollider;

    public AudioClip atkReflected, atkAbsorbed, shieldDestroyed, shieldRestored;
    public AudioSource ShieldAudioSource;
    public GameObject Boss;
    private BossController  bossController;

    public void shieldDestroy()
    {

        GameStats.bossShielded = false;
        shieldCollider.enabled =false;
        bossCollider.enabled = true;
        shieldSprite.enabled = false;
    }

    public void restoreShield()
    {
        GameStats.bossShielded = true;
        shieldCollider.enabled =true;
        bossCollider.enabled = false;
        shieldSprite.enabled = true;
    }

    public void restoreShieldHP()
    {
        shieldHP = MaxHP;
        shieldSprite.color = shieldFullColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossController = Boss.GetComponent<BossController>();
        GameStats.bossShielded = true;
        shieldHP = MaxHP;
        shieldSprite.color = shieldFullColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldHP <= 0.0f && GameStats.bossShielded)
        {
            ShieldAudioSource.PlayOneShot(shieldDestroyed, GameStats.gameVol);
            shieldDestroy();
        }
        else if(shieldHP > 0.0f && !GameStats.bossShielded)
        {
            ShieldAudioSource.PlayOneShot(shieldRestored, GameStats.gameVol);
            restoreShield();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.gameObject.layer == 13 && GameStats.isBattle)
        {
            if(coll.collider.gameObject != null)
            {
                if(coll.collider.gameObject.tag == "Projectile")
                {
                    shieldHP -= coll.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                    shieldSprite.color = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], shieldHP/MaxHP);
                    ShieldAudioSource.PlayOneShot(atkAbsorbed, GameStats.gameVol);
                }
                else if(coll.collider.gameObject.tag == "MeleeStrike") 
                {
                    ShieldAudioSource.PlayOneShot(atkReflected, GameStats.gameVol);
                }
            }
        }
    }

}

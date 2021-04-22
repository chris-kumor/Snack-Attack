using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldController : MonoBehaviour
{
    public float shieldHP, MaxHP;
    public SpriteRenderer shieldSprite;
    private Color shieldFullColor;
    public CapsuleCollider2D bossCollider;
    public CircleCollider2D shieldCollider;

    public AudioClip atkReflected, atkAbsorbed, shieldDestroyed, shieldRestored;
    public AudioSource ShieldAudioSource;
    public GameObject Boss;

    public Rigidbody2D BossRB2D;
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
        shieldHP = MaxHP;
        shieldSprite.enabled = true;
        shieldSprite.color = shieldFullColor;
        GameStats.bossShielded = true;
        shieldCollider.enabled =true;
        bossCollider.enabled = false;
        ShieldAudioSource.PlayOneShot(shieldRestored, GameStats.gameVol);
    }

    // Start is called before the first frame update
    void Start()
    {
        bossController = Boss.GetComponent<BossController>();
        GameStats.bossShielded = true;
        shieldHP = MaxHP;
        shieldFullColor = new Color(1.0f, 1.0f, 1.0f, 0.60f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStats.bossShielded && shieldHP <= 0.0f)
        {
            ShieldAudioSource.PlayOneShot(shieldDestroyed, GameStats.gameVol-0.1f);
            shieldDestroy();
        }

    
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(GameStats.isBattle)
        {
            if(coll.collider.gameObject.layer == 13)
            {
                if(coll.collider.gameObject != null)
                {
                    if(coll.collider.gameObject.tag == "Projectile")
                        ShieldAudioSource.PlayOneShot(atkReflected, GameStats.gameVol);

                    else if(coll.collider.gameObject.tag == "MeleeStrike") 
                    {
                        shieldHP -= coll.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                        shieldSprite.color = new Color(1.00f, 1.00f, 1.00f, (shieldHP/MaxHP)*0.6f);
                        ShieldAudioSource.PlayOneShot(atkAbsorbed, GameStats.gameVol);
                    }
                    
                }
            }
            //else if(coll.collider.gameObject.layer != 19 || coll.collider.gameObject.layer != 20)
                //BossRB2D.velocity += new Vector2(BossRB2D.velocity.x * 0.03f, BossRB2D.velocity.y * 0.03f);
        }
    }
}

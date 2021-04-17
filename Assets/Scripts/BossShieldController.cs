using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldController : MonoBehaviour
{
    public float shieldHP, MaxHP;
    public SpriteRenderer shieldSprite;
    private Color shieldFullColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    public CircleCollider2D bossCollider;

    public AudioClip atkReflected, atkAbsorbed, shieldDestroy;
    public AudioSource ShieldAudioSource;



    // Start is called before the first frame update
    void Start()
    {
        GameStats.bossShielded = true;
        shieldHP = MaxHP;
        shieldSprite.color = shieldFullColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldHP <= 0.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.gameObject.layer == 13 && GameStats.isBattle)
        {
            if(coll.collider.gameObject.tag == "Projectile")
            {
                shieldHP -= coll.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                shieldSprite.color = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], shieldSprite.color[3] - (Time.deltaTime/10));
                ShieldAudioSource.PlayOneShot(atkAbsorbed, 0.5f);
            }
            else if(coll.collider.gameObject.tag == "MeleeStrike") 
            {
                ShieldAudioSource.PlayOneShot(atkReflected, 0.5f);
            }
        }
    }

    void OnDestroy()
    {
        ShieldAudioSource.PlayOneShot(shieldDestroy, 0.5f);
        GameStats.bossShielded = false;
        bossCollider.enabled = true;
    }
}

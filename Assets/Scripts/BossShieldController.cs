using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldController : MonoBehaviour
{
    public float shieldHP, MaxHP;
    public SpriteRenderer shieldSprite;
    private Color shieldFullColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    public CircleCollider2D bossCollider;


    // Start is called before the first frame update
    void Start()
    {
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
        if(coll.collider.gameObject.layer == 13)
        {
            if(coll.collider.gameObject.tag == "Projectile")
                shieldHP -= coll.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
            else if(coll.collider.gameObject.tag == "MeleeStrike") 
                shieldHP -= coll.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
            shieldSprite.color = new Color(shieldSprite.color[0], shieldSprite.color[1], shieldSprite.color[2], shieldSprite.color[3] - (Time.deltaTime/10));
        }
    }

    void OnDestroy()
    {
        bossCollider.enabled = true;
    }
}

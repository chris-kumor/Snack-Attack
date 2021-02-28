using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;



public class BossController : MonoBehaviour
{
    private Rigidbody2D BossRB2D;
    public Camera BossLOS;
    public  string[] Preylabel = new string[2];
    public float BossVelocity;
    GameObject Prey;
    private float HP;
    public float MaxHP;
    public float minDist;
    public float angularSpeed;
    public AtkStruct[] attacks;
    private SpriteRenderer BossIdleSprite;

    public float GetHP()
    {
        return this.HP;
    }




    // Start is called before the first frame update
    void Start()
    {
        BossRB2D = gameObject.GetComponent<Rigidbody2D>();
        BossIdleSprite = gameObject.GetComponent<SpriteRenderer>();
        Prey = GameObject.FindWithTag(Preylabel[0]);
        this.HP = MaxHP;
    
    }
    void Update()
    {
        
    }

    void enableIdleSprite()
    {
        BossIdleSprite.enabled = true;
    }
    


    void FixedUpdate()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }

        if(Prey != null)
        {
            if(BossLOS.WorldToViewportPoint(Prey.transform.position).x > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).x < 1 &&BossLOS.WorldToViewportPoint(Prey.transform.position).y > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).y <1)
            {
                BossRB2D.angularVelocity *= 0.0f;
                float distance = Vector2.Distance(BossRB2D.transform.position, Prey.transform.position);
                Vector3 PreyDir = (Prey.transform.position - BossRB2D.transform.position);
                
                if( distance > minDist)
                {
                    BossRB2D.transform.position = Vector3.Lerp(BossRB2D.transform.position, Prey.transform.position, (BossVelocity * Time.deltaTime));
                }
                else if( distance <= minDist && attacks[0].canFire == true)
                {
                    
                    Debug.Log(PreyDir);
                    BossRB2D.velocity *= 0;
                    BossRB2D.angularVelocity *= 0.0f;
                    BossIdleSprite.enabled = false;
                    GameObject atk = PlayerController.Attack(attacks[0].atkObj, BossRB2D.transform.position, PreyDir, attacks[0].atkDistance, BossRB2D.transform.rotation);
                    atk = null;
                    Invoke("enableIdleSprite", 0.35f);
                    attacks[0].canFire = false; 
                }
            }
            
            else
            {
                /*BossRB2D.velocity = new Vector2(0.0f, 0.0f);
                Vector3 preyDirection =  Prey.transform.position -  gameObject.transform.position; 
                float singleStep = angularSpeed * Time.deltaTime;
                Vector3 lookDir = Vector3.RotateTowards(gameObject.transform.position, preyDirection, singleStep, 0.0f);
                BossRB2D.MoveRotation(Quaternion.LookRotation(lookDir, Vector3.forward));
                BossRB2D.transform.rotation = Quaternion.AngleAxis(0, Vector3.up); 
                BossRB2D.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);*/
            }             
        }
        else
        {
            Prey = GameObject.FindWithTag(Preylabel[1]);
        }

        if (attacks[0].cooldownTimer < 0 && attacks[0].canFire == false)
        {
            attacks[0].canFire = true;
            attacks[0].cooldownTimer = attacks[0].cooldown;
        }
        attacks[0].cooldownTimer -= Time.deltaTime;
            
        
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
            
            if(collision.collider.gameObject.tag == "Projectile")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                GameStats.RangedDamage += collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;;
            }
            else if(collision.collider.gameObject.tag == "MeleeStrike")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                GameStats.MeleeDamage += collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
            }
     

    }
}

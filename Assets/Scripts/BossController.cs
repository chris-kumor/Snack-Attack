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
    private Vector3 BossDir;
    public LayerMask layerMask;

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
        BossDir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 1.0f);
        BossRB2D.velocity = BossDir * (angularSpeed*100) * Time.deltaTime;
        
    
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
           float distance = Vector2.Distance(BossRB2D.transform.position, Prey.transform.position);
           Vector3 PreyDir = (Prey.transform.position - BossRB2D.transform.position);
           PreyDir = new Vector3(PreyDir.x, PreyDir.y, 0);
           PreyDir.Normalize();
            if(BossLOS.WorldToViewportPoint(Prey.transform.position).x > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).x < 1 &&BossLOS.WorldToViewportPoint(Prey.transform.position).y > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).y <1)
            {
                BossRB2D.angularVelocity *= 0.0f;
                
                if( distance <= minDist && attacks[0].canFire == true)
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

            
             int pickAtk = Random.Range(1, attacks.Length);
                if (attacks[pickAtk].canFire == true)
                {

                    
                    GameObject atk = PlayerController.Attack(attacks[pickAtk].atkObj, BossRB2D.transform.position, BossDir, attacks[pickAtk].atkDistance, BossRB2D.transform.rotation);
                    atk.transform.right = PreyDir;
                    atk = null;
                    
                    attacks[pickAtk].canFire = false;
                }            
        }
        else
        {
            Prey = GameObject.FindWithTag(Preylabel[1]);
        }

        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i].cooldownTimer < 0 && attacks[i].canFire == false)
            {
                attacks[i].canFire = true;
                attacks[i].cooldownTimer = attacks[i].cooldown;
            }
            attacks[i].cooldownTimer -= Time.deltaTime;
        }
        
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
            
            if(collision.collider.gameObject.tag == "Projectile")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                GameStats.RangedDamage += collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
            }
            else if(collision.collider.gameObject.tag == "MeleeStrike")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                GameStats.MeleeDamage += collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
            }
            else if(collision.collider.gameObject.layer == 10)
            {
                

            }
     

    }
}

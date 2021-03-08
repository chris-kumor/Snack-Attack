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
    private Quaternion currentRotation;

    public float GetHP()
    {
        return this.HP;
    }

        void enableIdleSprite()
    {
        BossIdleSprite.enabled = true;
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


    


    void FixedUpdate()
    {
        //Boss rotates in the dir its moving.
        if(BossRB2D.velocity.x < 0.0f && BossRB2D.velocity.y < 0.0f)
        {
            BossRB2D.rotation = 0.0f - Mathf.Atan2(BossRB2D.velocity.x, BossRB2D.velocity.y) * Mathf.Rad2Deg;
        }
        else
        {
            BossRB2D.rotation = 90.0f-Mathf.Atan2(BossRB2D.velocity.x, BossRB2D.velocity.y) * Mathf.Rad2Deg;
        }

        if(HP <= 0)
        {
            Destroy(gameObject);
        }

        //Making sure the player the boss is looking for is still in the game
        if(Prey != null)
        {
            // If the secondary camera that follows the Boss sees the target player
            if(BossLOS.WorldToViewportPoint(Prey.transform.position).x > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).x < 1 &&BossLOS.WorldToViewportPoint(Prey.transform.position).y > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).y <1)
            {
                BossRB2D.angularVelocity *= 0.0f;
                float distance = Vector2.Distance(BossRB2D.transform.position, Prey.transform.position);
                Vector3 PreyDir = (Prey.transform.position - BossRB2D.transform.position);
                //Knowing the direction and dist of the target if its clsoe enough launch an attakc in its dir
                if( distance <= minDist && attacks[0].canFire == true)
                {
                    //Debug.Log(PreyDir);
                    BossIdleSprite.enabled = false;
                    GameObject atk = PlayerController.Attack(attacks[0].atkObj, BossRB2D.transform.position, PreyDir, attacks[0].atkDistance, BossRB2D.transform.rotation);
                    atk = null;
                    Invoke("enableIdleSprite", 0.35f);
                    attacks[0].canFire = false; 
                }
                //if not then exit loop so we will check to see if the target is still in the scene
                else
                {
                    return;
                }
            }
                        
        }
        //Once the first terget is down look for next target
        else
        {
            Prey = GameObject.FindWithTag(Preylabel[1]);
        }

        //Allows Boss to attack again in x Seconds
        if (attacks[0].cooldownTimer < 0 && attacks[0].canFire == false)
        {
            attacks[0].canFire = true;
            attacks[0].cooldownTimer = attacks[0].cooldown;
        }
        //CoolDown for Bos Attack in action
        attacks[0].cooldownTimer -= Time.deltaTime;

        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
            //Depenind on the type of attack it will  damage the boss on damage carried by its script and store damage info to the right player
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
    }
}

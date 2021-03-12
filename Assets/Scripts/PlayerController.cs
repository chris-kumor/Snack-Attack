using System.Collections;
using System.Collections.Generic; 
using UnityEngine;



public class PlayerController : MonoBehaviour  
{
    public float speed;
    public float MaxHP;
    public AtkStruct[] attacks;
    public string vaxis, haxis, aimVAxis, aimHAxis;
    public GameObject AimSprite;
    private Rigidbody2D PlayerRB2D;
    private Transform pos;
    private Vector2 MoveDir;
    public AtkStruct shield;
    public GameObject playerShield;
    private SpriteRenderer Player_Sprite;
    private float playerHP;
    private GameObject atk;
    private Vector2 AimDir;
    private Quaternion AimAngle;
    
    
  
    



    public static GameObject Attack(GameObject Atk, Vector3 weaponPos, Vector3 targetDir, float atkDistance, Quaternion ProjectileRotation)
    {
        Vector3 atkPos = new Vector3(weaponPos.x + targetDir.x * atkDistance * Time.deltaTime, weaponPos.y + targetDir.y * atkDistance * Time.deltaTime, 1.00f);
        GameObject atk = Instantiate(Atk, atkPos, ProjectileRotation);
        return atk;
    }

    public float GetPlayerHP()
    {
        return this.playerHP;
    }

    void enableIdleSprite()
    {
        Player_Sprite.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerRB2D = gameObject.GetComponent<Rigidbody2D>();
        Player_Sprite = gameObject.GetComponent<SpriteRenderer>();
        pos = gameObject.GetComponent<Transform>();
        MoveDir = new Vector2(0, 0);
        AimDir = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
        this.playerHP = MaxHP;
        AimSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        //Health Check
        if(this.playerHP <= 0)
        {
            Destroy(gameObject);
        }

                 // Attacking
        for (int i = 0; i < attacks.Length; i++)
        {
            if(Input.GetAxis(attacks[i].fireKey) == 1 && attacks[i].canFire)
            {
                
                if(gameObject.tag == "MeleePlayer")
                {
                    Player_Sprite.enabled = false;
                }
                atk = Attack(attacks[i].atkObj, pos.position, AimDir,attacks[i].atkDistance, pos.rotation);
                atk.transform.right = new Vector3(AimDir.x, AimDir.y, 1.00f);
                atk = null;
                attacks[i].canFire = false;
                Invoke("enableIdleSprite",  0.3f);
            }

                        
            if (attacks[i].cooldownTimer < 0 && attacks[i].canFire == false)
            {
                attacks[i].canFire = true;
                attacks[i].cooldownTimer = attacks[i].cooldown;
            }
            attacks[i].cooldownTimer -= Time.deltaTime;
        }

    
    }

    // Update is called once per x frame
    void FixedUpdate()
    {

        //Movement
        if (Input.GetAxis(haxis) != 0 || Input.GetAxis(vaxis) != 0)
        {
            MoveDir = new Vector2(Input.GetAxis(haxis), Input.GetAxis(vaxis));
            MoveDir.Normalize();
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + (new Vector3(MoveDir.x,MoveDir.y, 1.00f)  * speed * Time.deltaTime));
        }
        else
        {
            PlayerRB2D.velocity *= 0;
        }

        if(Input.GetAxis(aimHAxis) != 0 || Input.GetAxis(aimVAxis) != 0)
        {
            AimDir = new Vector2(Input.GetAxis(aimHAxis), Input.GetAxis(aimVAxis));
            AimDir.Normalize();
            AimSprite.transform.position = gameObject.transform.position + (new Vector3(AimDir.x, AimDir.y, 1.0f) * 3);
            AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
            AimSprite.transform.rotation = AimAngle;
            AimSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            AimSprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float potentialDamage = 0.0f;
        if(collision.collider.gameObject.tag == "BossMeleeAtk")
        {
            potentialDamage= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage ;
        }
        else if(collision.collider.gameObject.tag == "BossRangeAtk")
        {
            potentialDamage= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
        }
        this.playerHP -= potentialDamage * playerShield.GetComponent<ShieldController>().isExposed;
    }


}

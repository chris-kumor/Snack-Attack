using System.Collections;
using System.Collections.Generic; 
using UnityEngine;



public class PlayerController : MonoBehaviour  
{
    public float speed;
    public float MaxHP;
    public AtkStruct[] attacks;
    public string vaxis;
    public string haxis;
    
    private Rigidbody2D PlayerRB2D;
    private Transform pos;
    private Vector2 MoveDir;
    private SpriteRenderer Player_Sprite;
    private float playerHP;
    
  
    



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
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
        this.playerHP = MaxHP;
        

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
            if (Input.GetKeyDown(attacks[i].fireKey) && attacks[i].canFire)
            {
                
                if(gameObject.tag == "MeleePlayer")
                {
                    Player_Sprite.enabled = false;
                }
                GameObject atk = Attack(attacks[i].atkObj, pos.position, MoveDir,attacks[i].atkDistance, pos.rotation);
                atk.transform.right = new Vector3(MoveDir.x, MoveDir.y, 1.00f);
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

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "BossMeleeAtk")
        {
            this.playerHP -= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
        }
        else if(collision.collider.gameObject.tag == "BossRangeAtk")
        {
            this.playerHP -= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
        }
    }


}

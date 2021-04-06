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
    public AtkStruct shield;
    public GameObject playerShield;
    public float AimReticleOffSet;

    private Rigidbody2D PlayerRB2D;
    private Transform pos;
    private Vector2 MoveDir;
    private SpriteRenderer Player_Sprite;
    private float playerHP;
    private GameObject atk;
    private Vector2 AimDir;
    private Quaternion AimAngle;
    private AudioSource PlayerAudioSource;
    private SinputSystems.InputDeviceSlot slot;
    private string PlayerTag;
    private bool isMouseAiming;


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

    public void ChangeHealth(string opSymbol, float amnt)
    {
        if(opSymbol == "+")
        {
            this.playerHP += amnt;
        }
        else if(opSymbol == "-")
        {
            this.playerHP -= amnt;
        }
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
        MoveDir = new Vector2(0.0f, 0.0f);
        AimDir = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
        this.playerHP = MaxHP;
        AimSprite.GetComponent<SpriteRenderer>().enabled = false;
        PlayerAudioSource = gameObject.GetComponent<AudioSource>();
        PlayerTag = gameObject.tag;
        if(PlayerTag == "MeleePlayer")
        {
            slot = GameStats.MeleeSlot;
        }
        else
        {
            slot = GameStats.RangedSlot;
        }
        
        if(slot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
        {
            isMouseAiming = true;
        }
        else{
            isMouseAiming = false;
        }

    }

    void Update()
    {


        //Health Check
        if(this.playerHP <= 0)
        {
            Destroy(gameObject);
        }
        else if(this.playerHP > MaxHP)
        {
            this.playerHP = MaxHP;
        }

                 // Attacking
        for (int i = 0; i < attacks.Length; i++)
        {
            if(Sinput.GetButtonDown(attacks[i].fireKey, slot) && attacks[i].canFire)
            {
                    
                if(gameObject.tag == "MeleePlayer")
                {
                    Player_Sprite.enabled = false;
                }
                PlayerAudioSource.PlayOneShot(attacks[i].soundToPlay, 0.2f);
                atk = Attack(attacks[i].atkObj, pos.position, AimDir, attacks[i].atkDistance, pos.rotation);
                atk.transform.right = new Vector3(AimDir.x, AimDir.y, 0f);
                atk = null;
                attacks[i].canFire = false;
                Invoke("enableIdleSprite",  0.3f);
            }
        
                        
            if(attacks[i].cooldownTimer < 0 && attacks[i].canFire == false)
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
        if (Sinput.GetAxis(haxis, slot) != 0 || Sinput.GetAxis(vaxis, slot) != 0)
        {
                
            MoveDir = new Vector2(Sinput.GetAxis(haxis, slot), Sinput.GetAxis(vaxis, slot));
            MoveDir.Normalize();
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + (new Vector3(MoveDir.x,MoveDir.y, 1.00f)  * speed * Time.deltaTime));
        }
        else
        {   
            PlayerRB2D.velocity *= 0;
        }

        Debug.Log(isMouseAiming);
        if(isMouseAiming)
        {
            
            AimDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
            AimDir.Normalize();
            AimSprite.transform.position = gameObject.transform.position + (Vector3)(AimDir * AimReticleOffSet);
            AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
            AimSprite.transform.rotation = AimAngle;
            AimSprite.GetComponent<SpriteRenderer>().enabled = true;
        }

        else if(isMouseAiming == false)
        {
            if(Sinput.GetAxis(aimHAxis, slot) != 0 || Sinput.GetAxis(aimVAxis ,slot) != 0)
            {
                AimDir = new Vector2(Sinput.GetAxis(aimHAxis, slot), Sinput.GetAxis(aimVAxis, slot));
                AimDir.Normalize();
                AimSprite.transform.position = gameObject.transform.position + (new Vector3(AimDir.x, AimDir.y, 1.0f) * AimReticleOffSet);
                AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
                AimSprite.transform.rotation = AimAngle;
                AimSprite.GetComponent<SpriteRenderer>().enabled = true;
            }

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
        else if(collision.collider.gameObject.tag == "BossRangeAttk")
        {
            potentialDamage= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
        }
        this.playerHP -= potentialDamage * playerShield.GetComponent<ShieldController>().isExposed;
    }


}

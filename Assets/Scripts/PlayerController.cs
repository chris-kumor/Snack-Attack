using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class PlayerController : MonoBehaviour  
{
    public float speed, MaxHP, reviveDist;
    public AtkStruct[] attacks;
    public AtkStruct shield;
    public GameObject playerShield, otherPlayer, AimSprite;
    public float AimReticleOffSet;
    public Animator Animator;
    public SpriteRenderer Player_Sprite;
    public float colorTime;
    public AudioClip PlayerDamaged, PlayerHealing, PlayerShield;
    public Rigidbody2D PlayerRB2D;
    public AudioSource PlayerAudioSource;
    public string PlayerTag;
    public bool isAlive;


    private Vector2 MoveDir, AimDir;
    private float playerHP, colorTimer;
    private GameObject atk;
    private Quaternion AimAngle;
    private SinputSystems.InputDeviceSlot slot;
    private bool isMouseAiming, isAttacking, isRanged;
    private Color desiredColor;

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

    public Vector2 GetAimDir()
    {
        return AimDir;
    }

    public void CanAttack()
    {
        isAttacking = false;
    }

    public bool attackStatus()
    {
        return isAttacking;
    }

    public void ChangeHealth(string opSymbol, float amnt)
    {
        if(opSymbol == "+")
            this.playerHP += amnt;
        else if(opSymbol == "-")
            this.playerHP -= amnt;
    }

    void enableIdleSprite()
    {
        Player_Sprite.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        MoveDir = new Vector2(0.0f, 0.0f);
        AimDir = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
        shield.cooldownTimer = shield.cooldown;
        AimSprite.GetComponent<SpriteRenderer>().enabled = false;
        this.playerHP = MaxHP;
        if(PlayerTag == "MeleePlayer")
            slot = GameStats.MeleeSlot;
        else
            slot = GameStats.RangedSlot;
        if(slot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
            isMouseAiming = true;
        else
            isMouseAiming = false;
        isAttacking = false;
        isAlive = true;
        if(gameObject.tag == "RangedPlayer")
            isRanged = true;
        else if(gameObject.tag != "RangedPlayer")
            isRanged = false;
        
    }

    void Update()
    {
        //Health Check
        if(this.playerHP <= 0)
        {
            PlayerRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            isAlive = false;
        }
        else
        {
            PlayerRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            isAlive = true;
        }
        if(this.playerHP > MaxHP)
            this.playerHP = MaxHP;
        
        //Visually notify PlayerDash
        if(Sinput.GetButtonDown("Dash", slot))
        {
            desiredColor = Color.blue;
            colorTimer = colorTime;
        }

        if(!otherPlayer.GetComponent<PlayerController>().isAlive && (Vector2.Distance(gameObject.transform.position, otherPlayer.transform.position) <= reviveDist) && Sinput.GetButtonDown("Revive", slot))
            otherPlayer.GetComponent<PlayerController>().ChangeHealth("+", 33.0f);

        // Detecting Attacking
        if(!isAttacking)
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                if(Sinput.GetButtonDown(attacks[i].fireKey, slot) && attacks[i].canFire)
                {
                    isAttacking = true;
                    if(gameObject.tag == "MeleePlayer")
                        Player_Sprite.enabled = false;
                    PlayerAudioSource.PlayOneShot(attacks[i].soundToPlay, 0.2f);
                    atk = Attack(attacks[i].atkObj, PlayerRB2D.position, AimDir, attacks[i].atkDistance, PlayerRB2D.transform.rotation);
                    atk.transform.right = new Vector3(AimDir.x, AimDir.y, 0f);
                    atk = null;
                    attacks[i].canFire = false;
                    Invoke("enableIdleSprite",  attacks[i].cooldown);
                    
                }

                if(attacks[i].cooldownTimer < 0 && attacks[i].canFire == false)
                {
                    attacks[i].canFire = true;
                    attacks[i].cooldownTimer = attacks[i].cooldown;
                }
                attacks[i].cooldownTimer -= Time.deltaTime;
            
            }
        }

        //Detecting Movement
        if ((Sinput.GetAxis("Horizontal", slot) != 0 || Sinput.GetAxis("Vertical", slot) != 0) && (!isAttacking || isRanged) && isAlive)
        {
                
            MoveDir = new Vector2(Sinput.GetAxis("Horizontal", slot), Sinput.GetAxis("Vertical", slot));
            MoveDir.Normalize();
            Animator.SetFloat("speed", (Mathf.Abs(MoveDir.x) + Mathf.Abs(MoveDir.y)));
            if(MoveDir.x < 0.0f )
                Player_Sprite.flipX = true;
            else if(MoveDir.x > 0.0f )
                Player_Sprite.flipX = false;
        }
        else
        {   
            PlayerRB2D.velocity *= 0;
            Animator.SetFloat("speed", 0.0f);
        }

        //Mouse AimiNG
        if(isMouseAiming  && (!isAttacking || isRanged) && isAlive)
        {
            
            AimDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
            AimDir.Normalize();
            AimSprite.transform.position = gameObject.transform.position + (Vector3)(AimDir * AimReticleOffSet);
            AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
            AimSprite.transform.rotation = AimAngle;
            AimSprite.GetComponent<SpriteRenderer>().enabled = true;
        }

        //Controllr Aiming
        else if(isMouseAiming == false && (!isAttacking || isRanged) && isAlive)
        {
            if(Sinput.GetAxis("Look Horizontal", slot) != 0 || Sinput.GetAxis("Look Vertical" ,slot) != 0)
            {
                AimDir = new Vector2(Sinput.GetAxis("Look Horizontal", slot), Sinput.GetAxis("Look Vertical", slot));
                AimDir.Normalize();
                AimSprite.transform.position = gameObject.transform.position + (new Vector3(AimDir.x, AimDir.y, 1.0f) * AimReticleOffSet);
                AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
                AimSprite.transform.rotation = AimAngle;
                AimSprite.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
            AimSprite.GetComponent<SpriteRenderer>().enabled = false;



    }

    // Update is called once per x frame
    void FixedUpdate()
    {
        //Flashing Player whatever color 
        if (colorTimer > 0)
            colorTimer -= Time.deltaTime;
        Player_Sprite.color = Color.Lerp(Color.white, desiredColor, colorTimer / colorTime);

   
        //Movement
        if ((Sinput.GetAxis("Horizontal", slot) != 0 || Sinput.GetAxis("Vertical", slot) != 0) && (!isAttacking || isRanged) && isAlive)
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + (new Vector3(MoveDir.x,MoveDir.y, 1.00f)  * speed * Time.deltaTime));
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float potentialDamage = 0.0f;
        if(isAlive)
        {
            if((collision.collider.gameObject.tag == "BossMeleeAtk" || collision.collider.gameObject.tag == "BossRangeAttk") && playerShield.gameObject.GetComponent<ShieldController>().isExposed == 1)
            {
                desiredColor = Color.red;
                colorTimer = colorTime;
                if(collision.collider.gameObject.tag == "BossMeleeAtk" )
                    potentialDamage= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                else if(collision.collider.gameObject.tag == "BossRangeAttk")
                    potentialDamage= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                PlayerAudioSource.PlayOneShot(PlayerDamaged, 0.4f);
            }
            else if(collision.gameObject.tag == "PickUp" && playerHP != MaxHP)
            {
                desiredColor = Color.yellow;
                colorTimer = colorTime;
                PlayerAudioSource.PlayOneShot(PlayerHealing, 0.5f);

            }
            
            else if(collision.gameObject.tag == "ShieldPickUp" && shield.cooldownTimer != shield.cooldown)
            {
                desiredColor = Color.green;
                colorTimer = colorTime;
                PlayerAudioSource.PlayOneShot(PlayerShield, 0.5f);
            }
            
            this.playerHP -= potentialDamage * playerShield.gameObject.GetComponent<ShieldController>().isExposed;
        }
    }


}

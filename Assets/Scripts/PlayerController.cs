using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEditor;
using static AttackCOntroller;

public class PlayerController : MonoBehaviour  
{
    public float speed, MaxHP, reviveDist, playerHP;
    public AtkStruct[] attacks;
    public AtkStruct shield;
    public GameObject playerShield, AimSprite;
    public float AimReticleOffSet;
    public Animator Animator;
    public SpriteRenderer Player_Sprite;
    public float colorTime;
    public AudioClip PlayerDamaged, PlayerHealing, PlayerShield;
    public Rigidbody2D PlayerRB2D;
    public AudioSource PlayerAudioSource;
    public string otherPlayerTag;
    public bool isAlive, AimSpriteEnabled, isDashing, isMoving;


    private Vector2 MoveDir, AimDir;
    private float colorTimer, otherPlayerHP;
    private GameObject atk, otherPlayer;
    private Quaternion AimAngle;
    private SinputSystems.InputDeviceSlot slot;
    private bool isMouseAiming, isAttacking, isRanged, otherPlayerAlive, isMelee;
    private Color desiredColor;
    private ShieldController shieldController;
    private PlayerController otherPlayerController;
 



    public void PlayerMovement(string HSmartControl, string VSmartControl)
    {   
         //Detecting Movement
        if ((Sinput.GetAxis(HSmartControl, slot) != 0 || Sinput.GetAxis(VSmartControl, slot) != 0) && (!isAttacking || isRanged) && isAlive)
        {
            isMoving = true;    
            MoveDir = Sinput.GetVector(HSmartControl, VSmartControl, slot);
            MoveDir.Normalize();
            Animator.SetFloat("speed", 1.0f);
            if(MoveDir.x < 0.0f )
                Player_Sprite.flipX = true;
            else if(MoveDir.x > 0.0f )
                Player_Sprite.flipX = false;
            if(GameStats.bothPlayersKB && isMelee)
            {
                AimDir = MoveDir;
                UpdateAimSpriteTransform();
            }
        }
        else
        {   
            isMoving = false;
            PlayerRB2D.velocity *= 0;
            Animator.SetFloat("speed", 0.0f);
        }
    }
    public void UpdateAimSpriteTransform()
    {
        AimDir.Normalize();
        AimSprite.transform.position = gameObject.transform.position + (Vector3)(AimDir * AimReticleOffSet);
        AimAngle.eulerAngles = new Vector3(0.0f, 0.0f, 180 - (Mathf.Atan2(AimDir.x, AimDir.y) * Mathf.Rad2Deg));
        AimSprite.transform.rotation = AimAngle;
        AimSprite.SetActive(true);
    }

    public void MouseAim()
    {
        //Mouse AimiNG
        if(isMouseAiming  && (!isAttacking || isRanged) && isAlive && !isDashing)
        {
            AimDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
            UpdateAimSpriteTransform();
        }
    }

    public Vector2 GetAimDir()
    {
        return AimDir;
    }

    public void CanAttack()
    {
        isAttacking = false;
    }

    public Vector2 GetMoveDir()
    {
        return MoveDir;
    }

    public bool attackStatus()
    {
        return isAttacking;
    }

    void enableIdleSprite()
    {
        Player_Sprite.enabled = true;
    }

    public void findPlayers()
    {
        otherPlayer = GameObject.FindWithTag(otherPlayerTag);
        otherPlayerController = otherPlayer.GetComponent<PlayerController>();
        otherPlayerHP = otherPlayer.GetComponent<PlayerController>().playerHP;
    }


    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        isRanged = false;
        isMelee = false;
        isMouseAiming = false;
        MoveDir = new Vector2(0.0f, 0.0f);
        AimDir = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
        shield.cooldownTimer = shield.cooldown;
        findPlayers();
        shieldController = playerShield.gameObject.GetComponent<ShieldController>();
        AimSprite.SetActive(false);
        this.playerHP = MaxHP;
        isAttacking = false;
        isAlive = true;
        isDashing = false;
        if(gameObject.tag == "RangedPlayer")
        {
            isRanged = true;
            slot = GameStats.RangedSlot;
        }
        else if(gameObject.tag == "MeleePlayer")
        {
            isMelee = true;
            slot = GameStats.MeleeSlot;
        }
        if(slot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
            isMouseAiming = true;

    }

    void Update()
    {
        Animator.SetBool("isDashing", isDashing);
        //Debug.Log(Vector2.Distance(gameObject.transform.position, otherPlayer.transform.position));
        //Health Check
        if(this.playerHP <= 0)
        {
            PlayerRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            isAlive = false;
        }
        else if(this.playerHP > 0)
        {
            PlayerRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            isAlive = true;
        }
        if(this.playerHP > MaxHP)
            this.playerHP = MaxHP;
        if(PlayerRB2D.velocity == Vector2.zero)
        {
            if(AimDir.x < 0.0f )
                Player_Sprite.flipX = true;
            else if(AimDir.x > 0.0f )
                Player_Sprite.flipX = false;
        }
        if(!(otherPlayerController.isAlive) && (Vector2.Distance(otherPlayer.transform.position, gameObject.transform.position) <= reviveDist))
        {
            bool isRevive = false;
            if(!GameStats.bothPlayersKB)
                isRevive = Sinput.GetButtonDown("Revive", slot);
            else if(GameStats.bothPlayersKB)
            {
                if(isMelee)
                    isRevive = Sinput.GetButtonDown("Revive", slot);
                else
                    isRevive = Sinput.GetButtonDown("AltRRevive", slot);
            }
            if(isRevive)
                otherPlayerController.playerHP += 33.0f;
        }
        // Detecting Attacking
        if(!isAttacking && isAlive && !isDashing && !GameStats.isPaused)
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                bool isNormAtk;
                if(!GameStats.bothPlayersKB)
                    isNormAtk = (Sinput.GetButtonDown(attacks[i].fireKey, slot) && attacks[i].canFire);
                else
                    isNormAtk = (Sinput.GetButtonDown(attacks[i].altFireKey, slot) && attacks[i].canFire);

                if(isNormAtk)
                {
                    isAttacking = true;
                    if(gameObject.tag == "MeleePlayer")
                        Player_Sprite.enabled = false;
                    PlayerAudioSource.PlayOneShot(attacks[i].soundToPlay, GameStats.gameVol);
                    atk = AttackCOntroller.Attack(attacks[i].atkObj, PlayerRB2D.position, AimDir, attacks[i].atkDistance, PlayerRB2D.transform.rotation);
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


            //Player Movement
        if(GameStats.bothPlayersKB && !GameStats.isOnline){
            if(isMelee){
                PlayerMovement("Horizontal", "Vertical");
            }
            else if(isRanged){
                PlayerMovement("AltRHorizontal", "AltRVertical");
                MouseAim();
            }
        }
        else{
                PlayerMovement("Horizontal", "Vertical");
        }
        //Controller Aiming
        if(!isMouseAiming  && (!isAttacking || isRanged) && isAlive && !isDashing && !GameStats.bothPlayersKB){
            if(Sinput.GetAxis("Look Horizontal", slot) != 0 || Sinput.GetAxis("Look Vertical" ,slot) != 0){
                AimDir = Sinput.GetVector("Look Horizontal","Look Vertical", slot);
                UpdateAimSpriteTransform();
            }
        }
       // else
           // AimSprite.SetActive(false);
    }

    // Update is called once per x frame
    void FixedUpdate(){
        //Flashing Player whatever color 
        if (colorTimer > 0)
            colorTimer -= Time.deltaTime;
        Player_Sprite.color = Color.Lerp(Color.white, desiredColor, colorTimer/colorTime);

   
        //Movement
        if (isMoving && (!isAttacking || isRanged) && isAlive)
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + (new Vector3(MoveDir.x,MoveDir.y, 1.00f)  * speed * Time.deltaTime));
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        float potentialDamage = 0.0f;
        if(isAlive && collision.collider.gameObject != null){
            if(collision.collider.gameObject.layer == 16 && shieldController.isExposed == 1){
                desiredColor = Color.red;
                colorTimer = colorTime;
                if(collision.collider.gameObject.tag == "BossMeleeAtk" )
                    potentialDamage= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
    
                else if(collision.collider.gameObject.tag == "BossRangeAttk")
                    potentialDamage= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;

                else if(collision.collider.gameObject.tag == "BossSpreadAtk")
                    potentialDamage= collision.collider.gameObject.GetComponent<UpdateShellCounter>().attack.damage;

                PlayerAudioSource.PlayOneShot(PlayerDamaged, GameStats.gameVol); 
            }
            else if(collision.gameObject.layer == 15 && playerHP != MaxHP){
                desiredColor = Color.yellow;
                colorTimer = colorTime;
                PlayerAudioSource.PlayOneShot(PlayerHealing, GameStats.gameVol);
            }
            
            else if(collision.gameObject.layer == 9 && shield.cooldownTimer != shield.cooldown && shieldController.isExposed == 1){
                desiredColor = Color.green;
                colorTimer = colorTime;
                PlayerAudioSource.PlayOneShot(PlayerShield, GameStats.gameVol);
            }
 
            this.playerHP -= potentialDamage * shieldController.isExposed;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackCOntroller;




public class BossController : MonoBehaviour
{
    
    public float MaxHP, HP, FOV, minDist, maxDist, lookRadius, angularSpeed, shakeTime, peakTime, peak,  colorTime, timeToAttack;
    public AtkStruct[] attacks;
    public AudioClip BossDamaged, BossStuned;
    public PhysicsMaterial2D PureBounce;
    public Rigidbody2D BossRB2D;
    public AudioSource BossAudioSource;
    public SpriteRenderer BossSprite;
    GameObject[] Prey;
    public Animator bossAnimator;
    public GameObject MainCamera;
    public CapsuleCollider2D bossCapsuleCollider;
    public bool isAttacking;

    private Vector2 BossDir, prevVelocity;
    private float timer, colorTimer;
    private Vector3 PreyDir;
    private Collider2D[] visibleEnemies;
    private PlayerController MeleeController, RangedController, enemyController;
    private BossShieldController bossShieldController;
    private CameraController MainCamController;
    public GameObject BossShield;
    private bool  isPhase1, isStunned;
    private string EnemyTag;
    private LayerMask layerMask;

   public void UnFreezeBoss()
   {
       BossRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
       BossRB2D.velocity = prevVelocity;

   }
 
    /*Unfreezes Boss 
    triggers Phase 1 Attack:
    Wait X secs before we allow Boss to trigger attack code in FixedUpdate()
    */
    public void StartBattle()
    {
        GameStats.isBattle = true;
        BossRB2D.isKinematic = false;
        Phase1Attack();
        Invoke("CanAttack", timeToAttack);
    }

    public Vector2 GetPreyDir()
    {
        return PreyDir;
    }

    void enableIdleSprite()
    {
        BossSprite.enabled = true;
    }

    public void CanAttack()
    {
        isAttacking = false;
    }

    void Phase1Attack()
    {
        //Making sure boss cnt be attacked whiels shielded
        bossCapsuleCollider.enabled = false;
        //Assign boss rndom Direction at angularSpeed;
        BossDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        BossDir.Normalize();
        BossRB2D.velocity = new Vector2(BossDir.x * angularSpeed, BossDir.y * angularSpeed);
    }

    void seekTargets()
    {
        Prey[0] = GameObject.FindWithTag("RangedPlayer");
        Prey[1] = GameObject.FindWithTag("MeleePlayer");
        layerMask = LayerMask.GetMask("MeleePlayer","RangedPlayer");
    }
    IEnumerator UnStunBoss()
    {   
        yield return new WaitForSeconds(5.00f);
        bossShieldController.restoreShield();
        UnFreezeBoss();
        isStunned = false;
    }

    IEnumerator StopBossAndWait(float waitTime, int attackNum)
    {
        
        PreyDir.Normalize();
        BossRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        BossAudioSource.PlayOneShot(attacks[attackNum].soundToPlay, GameStats.gameVol);
        GameObject atk = AttackCOntroller.Attack(attacks[attackNum].atkObj, BossRB2D.transform.position + 2*PreyDir, PreyDir, attacks[attackNum].atkDistance, BossRB2D.transform.rotation);
        atk.transform.right = new Vector3(PreyDir.x, PreyDir.y, 0f);
        atk = null;
        attacks[attackNum].canFire = false;
        yield return new WaitForSeconds(waitTime);

    }
    // Start is called before the first frame update
    void Start()
    {
        seekTargets();
        this.HP = MaxHP;
        colorTimer = 0.0f;   
        timer = peakTime;
        MeleeController= Prey[0].GetComponent<PlayerController>();
        RangedController = Prey[1].GetComponent<PlayerController>();
        MainCamController = MainCamera.GetComponent<CameraController>();
        bossShieldController = BossShield.GetComponent<BossShieldController>();
        isAttacking = true;
        isPhase1 = true;
        isStunned = false;
        for(int i = 0; i < attacks.Length; i++)
            if(i ==0)
                attacks[i].cooldownTimer = attacks[i].cooldown + 2.00f;
            else
                attacks[i].cooldownTimer = attacks[i].cooldown;
    }

    void Update()
    {   
        if(Prey[0] == null || Prey[1] == null)
            seekTargets();
        //Debug.Log(this.HP);
        //CoolDown for Bos Attack in action
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].cooldownTimer -= Time.deltaTime;
            //Allows Boss to attack again in x Seconds
            if (attacks[i].cooldownTimer <= 0.0f && attacks[i].canFire == false)
            {
                attacks[i].canFire = true;
                if(i == 0)
                    attacks[i].cooldownTimer = attacks[i].cooldown + 4.00f;
                else
                    attacks[i].cooldownTimer = attacks[i].cooldown;
            }
        }

        //Boss Damaged, buffed, etc: Boss sprite renderer Color changes in value and below is timer to keep track of time before Color is back to normal
        if (colorTimer > 0)
            colorTimer -= Time.deltaTime;
        BossSprite.color = Color.Lerp(Color.white, Color.red, colorTimer / colorTime);
    }

    void FixedUpdate()
    {
        if (GameStats.isBattle)
        {
            
            if(this.HP < 1.00f)
                Destroy(gameObject);
            //flipping sprite
            bossAnimator.SetFloat("speed", (Mathf.Abs(BossRB2D.velocity.magnitude)));
            if((BossRB2D.velocity.x < 0.0f || PreyDir.x < 0.0f) && !isStunned)
                BossSprite.flipX = false;
            else if((BossRB2D.velocity.x > 0.0f || PreyDir.x > 0.0f) && isStunned)
                BossSprite.flipX = true;
            bossAnimator.SetBool("isAttacking", isAttacking);
        
            visibleEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, lookRadius, layerMask);
            //Making sure the player the boss is looking for is still in the game
            if ((MeleeController.isAlive || RangedController.isAlive) && !isAttacking && !isStunned)
            {
                for(int i = 0; i < visibleEnemies.Length; i++)
                {
                    if(!isAttacking)
                    {
                        //Debug.Log(visibleEnemies[i].gameObject.layer);
                        if(visibleEnemies[i].tag == "MeleePlayer")
                            enemyController = MeleeController;
                        else
                            enemyController = RangedController;
                        float angle = Vector2.Angle(BossRB2D.velocity, visibleEnemies[i].gameObject.transform.right);
                        float distance = Vector2.Distance(BossRB2D.transform.position, visibleEnemies[i].gameObject.transform.position);
                        PreyDir = (visibleEnemies[i].gameObject.transform.position - BossRB2D.transform.position);
                        //RotateBossToFace(visibleEnemies[i].gameObject.transform.position);
                        prevVelocity = BossRB2D.velocity;
                        if (enemyController.isAlive && angle <= FOV && distance <= minDist && attacks[0].canFire && !isAttacking) 
                        {
                            isAttacking = true;
                            BossSprite.enabled = false;
                            StartCoroutine(StopBossAndWait(attacks[0].cooldownTimer, 0));
                        }
                        //if not then exit loop so we will check to see if the target is still in the scene
                        else if (distance > minDist && distance < maxDist && attacks[1].canFire == true)
                        {
                            isAttacking = true;
                            BossSprite.enabled = false;
                            StartCoroutine(StopBossAndWait(1.00f, 1));
                            UnFreezeBoss();
                            enableIdleSprite();    
                        }
                        else if (distance > maxDist && attacks[2].canFire == true)
                        {
                            isAttacking = true;
                            BossSprite.enabled = false;
                            StartCoroutine(StopBossAndWait(1.00f, 2));
                            UnFreezeBoss();
                            enableIdleSprite();

                        }
                    }
                }
            }
        }
        else
            BossRB2D.isKinematic = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
   
        {     if (GameStats.isBattle && !GameStats.bossShielded)
            //Depenind on the type of attack it will  damage the boss on damage carried by its script and store damage info to the right player
            if (collision.collider.gameObject.tag == "Projectile")
            {
                PlayerShotController playerAttackController = collision.collider.gameObject.GetComponent<PlayerShotController>();
                this.HP -= playerAttackController.attack.damage;
                GameStats.RangedDamage += playerAttackController.attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, GameStats.gameVol);
                colorTimer = colorTime;
            }
            else if (collision.collider.gameObject.tag == "MeleeStrike")
            {
                PlayerStrikeController playerAttackController = collision.collider.gameObject.GetComponent<PlayerStrikeController>();
                GameStats.shakeMagnitude = 0.2f;
                if(playerAttackController.attack.fireKey == "Fire2")
                    GameStats.ShakeTime = shakeTime;
                this.HP -= playerAttackController.attack.damage;
                GameStats.MeleeDamage += playerAttackController.attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, GameStats.gameVol);
                colorTimer = colorTime;
            }
            
            else if(collision.collider.gameObject.layer == 10  && isPhase1 && !GameStats.bossShielded)
            {
                BossAudioSource.PlayOneShot(BossStuned, GameStats.gameVol);
                BossRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
                isStunned = true;
                GameStats.shakeMagnitude = 0.4f;
                GameStats.ShakeTime = shakeTime * 2;
                StartCoroutine(UnStunBoss());

            }
        }
    }
}

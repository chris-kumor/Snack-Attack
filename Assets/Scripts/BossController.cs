using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;




public class BossController : MonoBehaviour
{
    
    public float MaxHP, HP, FOV, minDist, maxDist, lookRadius, angularSpeed, shakeTime, peakTime, peak,  colorTime, bossMass;
    public AtkStruct[] attacks;
    public AudioClip BossDamaged, BossStuned;
    public PhysicsMaterial2D PureBounce;
    public Rigidbody2D BossRB2D;
    public AudioSource BossAudioSource;
    public SpriteRenderer BossSprite;
    public GameObject[] Prey;
    public Animator bossAnimator;
    public GameObject MainCamera;
    public CircleCollider2D bossCircleCollider;
    public bool isAttacking;

    private Vector2 BossDir;
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

   void restoreshieldHP()
   {
      bossShieldController.restoreShieldHP();
   }

   public void UnFreezeBoss()
   {
       BossRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
       Debug.Log("Calling Phase 1 Attack");
       Phase1Attack();

   }
 
    public void StartBattle()
    {
        GameStats.isBattle = true;
        BossRB2D.isKinematic = false;
        BossRB2D.mass = bossMass;
        Phase1Attack();
    }

    public Vector2 GetPreyDir()
    {
        return PreyDir;
    }

    void enableIdleSprite()
    {
        BossSprite.enabled = true;
    }

    void RotateBossToFace(Vector2 target)
    {
        Quaternion Looking = Quaternion.identity;
        Looking.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg);
        gameObject.transform.rotation = Looking;
    }

    public void CanAttack()
    {
        isAttacking = false;


    }

    void Phase1Attack()
    {
        //Debug.Log("Im in Phase 1");
        bossCircleCollider.enabled = false;
        BossDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        BossDir.Normalize();
        BossRB2D.velocity = new Vector2(BossDir.x * angularSpeed, BossDir.y * angularSpeed);
        RotateBossToFace(BossRB2D.velocity);
    }
    /*
    void Phase2Attack()
    {
        //Debug.Log("Im in Phase 2");
        //trigmethod
        //Oscillation on one dimension = wave ocsillation where wave propogates in dimension T to oscillating dimension
        //Oscillation in 2-D
        //Obj oscillates on same trig func in both dimensions, obj oscillates between a start and end point on resulted vector
        //WHen Obj oscillates on both dimensions with using sin() and cos() respiectivelly results in circular oscillation 
        //where the diamater is equal to the peak in the oscillation shared by bnoth trig funcs
        //peak = 2.50f;
        //BossRB2D.velocity = new Vector2(peak * Mathf.Cos(Time.time), peak * Mathf.Cos(Time.time));
        //modulusmethod
        //Oscillation is hard coded as Base of Value of timer @ some inst point in time mod Time it takes to reach crest/trough result is multiplied by velocity variable
        //This speed variable can help us control how much ground before x time until we go back in the opposite direction for the same amount of time.
        float oscillationWithModulus = (Mathf.Pow(2, Time.time) % 7 )* angularSpeed;
        if(timer > 0.0f )
        {
            if(timerIteration == 1)
            {
                BossRB2D.velocity = new Vector3 (oscillationWithModulus, -oscillationWithModulus, 0.0f);
                timerIteration = 2;
            }
            else if(timerIteration == 3)
            {
                BossRB2D.velocity = new Vector3 (-oscillationWithModulus, oscillationWithModulus, 0.0f);
                timerIteration = 4;
            }
        }
        else if(timer >= (-peakTime))
        {
            if(timerIteration == 2)
            {
                BossRB2D.velocity = new Vector3 (-oscillationWithModulus, -oscillationWithModulus, 0.0f);
                timerIteration = 3;
            }
            else if(timerIteration == 4)
            {
                BossRB2D.velocity = new Vector3 (oscillationWithModulus, oscillationWithModulus, 0.0f);
                timerIteration = 1;
            }
        }
        else
            timer = peakTime;
        timer -= Time.deltaTime;
    }

    void Phase3Attack()
    {
        //Debug.Log("Im in Phase 3");

    }
    */

    IEnumerator StopBossAndWait(float waitTime, int attackNum)
    {
        PreyDir.Normalize();
        BossRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        BossAudioSource.PlayOneShot(attacks[attackNum].soundToPlay, GameStats.gameVol);
        GameObject atk = PlayerController.Attack(attacks[attackNum].atkObj, BossRB2D.transform.position + 2*PreyDir, PreyDir, attacks[attackNum].atkDistance, BossRB2D.transform.rotation);
        atk.transform.right = new Vector3(PreyDir.x, PreyDir.y, 0f);
        atk = null;
        attacks[attackNum].canFire = false;
        yield return new WaitForSeconds(waitTime);

    }
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("MeleePlayer","RangedPlayer");
        this.HP = this.MaxHP;
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
        Invoke("CanAttack", 5.00f);
    }

    void Update()
    {   
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

        if (colorTimer > 0)
            colorTimer -= Time.deltaTime;

        BossSprite.color = Color.Lerp(Color.white, Color.red, colorTimer / colorTime);
    }

    void FixedUpdate()
    {
        if (GameStats.isBattle)
        {
            /*if((2*(MaxHP/3)) > this.HP && this.HP >= (MaxHP/3))
                Phase2Attack();
            //else if(this.MaxHP/3 > this.HP && this.HP >= 1.00f)
                Phase3Attack();
            */
            if(this.HP < 1.00f)
                Destroy(gameObject);
            //flipping sprite
            bossAnimator.SetFloat("speed", (Mathf.Abs(BossRB2D.velocity.magnitude)));
            if((BossRB2D.velocity.x < 0.0f || PreyDir.x < 0.0f) && !isStunned)
                BossSprite.flipX = false;
            else if((BossRB2D.velocity.x > 0.0f || PreyDir.x > 0.0f) && isStunned)
                BossSprite.flipX = true;
            if(BossRB2D.velocity == Vector2.zero)
                bossAnimator.SetFloat("speed", 0.0f);
            bossAnimator.SetBool("isAttacking", isAttacking);
            gameObject.transform.right = BossRB2D.velocity;
        
            visibleEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, lookRadius, layerMask);
            //Making sure the player the boss is looking for is still in the game
            if ((MeleeController.isAlive || RangedController.isAlive) && !isAttacking && !isStunned && visibleEnemies.Length != 0)
            {
                for(int i = 0; i < visibleEnemies.Length; i++)
                {
                    if(!isAttacking)
                    {
                        Debug.Log(visibleEnemies[i].gameObject.layer);
                        if(visibleEnemies[i].tag == "MeleePlayer")
                            enemyController = MeleeController;
                        else
                            enemyController = RangedController;
                        float angle = Vector2.Angle(BossRB2D.velocity, visibleEnemies[i].gameObject.transform.right);
                        float distance = Vector2.Distance(BossRB2D.transform.position, visibleEnemies[i].gameObject.transform.position);
                        PreyDir = (visibleEnemies[i].gameObject.transform.position - BossRB2D.transform.position);
                        //RotateBossToFace(visibleEnemies[i].gameObject.transform.position);
                        if (enemyController.isAlive && angle <= FOV && distance <= minDist && attacks[0].canFire && !isAttacking && !GameStats.bossShielded) 
                        {
                            isAttacking = true;
                            BossSprite.enabled = false;
                            StartCoroutine(StopBossAndWait(attacks[0].cooldownTimer, 0));
                        }
                        //if not then exit loop so we will check to see if the target is still in the scene
                        else if (distance > minDist && distance < maxDist && attacks[1].canFire == true)
                        {
                            isAttacking = true;
                            StartCoroutine(StopBossAndWait(1.00f, 1));
                            UnFreezeBoss();     
                        }
                        else if (distance > maxDist && attacks[2].canFire == true)
                        {
                            isAttacking = true;
                            StartCoroutine(StopBossAndWait(1.00f, 2));
                            UnFreezeBoss();

                        }
                        enableIdleSprite();
                        
                    }
                }
            }
        }
        else
            BossRB2D.isKinematic = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameStats.isBattle && !GameStats.bossShielded)
        {
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
                MainCamController.shakeMagnitude = 0.2f;
                if(playerAttackController.attack.fireKey == "Fire2")
                    MainCamController.timer = shakeTime;
                this.HP -= playerAttackController.attack.damage;
                GameStats.MeleeDamage += playerAttackController.attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, GameStats.gameVol);
                colorTimer = colorTime;
            }
            
            else if(collision.collider.gameObject.layer == 10 && isPhase1 && !GameStats.bossShielded)
            {
                BossAudioSource.PlayOneShot(BossStuned, GameStats.gameVol);
                BossRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
                isStunned = true;
                MainCamController.shakeMagnitude = 0.4f;
                MainCamController.timer = shakeTime * 2;
                Invoke("restoreshieldHP", 5.0f);
                Invoke("UnFreezeBoss", 5.0f);
                Invoke("Phase1Attack", 5.0f);
            }
        }
    }
}

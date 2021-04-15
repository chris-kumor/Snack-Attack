using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;




public class BossController : MonoBehaviour
{

    public float MaxHP, HP, FOV;
    public float minDist, maxDist, lookRadius;
    public float angularSpeed, shakeTime;
    public AtkStruct[] attacks;
    public float peakTime;
    public AudioClip BossDamaged, BossDying;
    public PhysicsMaterial2D PureBounce;
    public Rigidbody2D BossRB2D;
    public AudioSource BossAudioSource;
    public SpriteRenderer BossSprite;
    private GameObject[] Prey = new GameObject[2];
    public Animator bossAnimator;
    public GameObject MainCamera;
    public CircleCollider2D bossCircleCollider;




    private Vector2 BossDir;
    public float peak;
    private float timer;
    private Vector3 PreyDir;
    public float colorTime;
    private float colorTimer;
    private Vector2 prevVelocity;
    private PhysicsMaterial2D BossColliderMaterial;
    private Collider2D[] visibleEnemies;
    private PlayerController MeleeController, RangedController;
    private CameraController MainCamController;
    private  int timerIteration = 1;
    private bool isAttacking;


  
    public void StartBattle()
    {
        GameStats.isBattle = true;
        BossRB2D.isKinematic = false;
        Phase1Attack();
    }

    void enableIdleSprite()
    {
        BossSprite.enabled = true;
    }

    void Waiting()
    {

    }

    void RotateBossToFace(Vector2 target)
    {
        Quaternion Looking = Quaternion.identity;
        Looking.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg);
        gameObject.transform.rotation = Looking;
    }

    IEnumerator StopBossAndWait(float waitTime, int attackNum)
    {
        PreyDir.Normalize();
        //RotateBossToFace(PreyDir);
        prevVelocity = BossRB2D.velocity;
        BossRB2D.velocity = Vector2.zero;
        BossRB2D.constraints = RigidbodyConstraints2D.FreezePosition;
        BossAudioSource.PlayOneShot(attacks[attackNum].soundToPlay, 0.05f);
        GameObject atk = PlayerController.Attack(attacks[attackNum].atkObj, BossRB2D.transform.position + 2*PreyDir, PreyDir, attacks[attackNum].atkDistance, BossRB2D.transform.rotation);
        atk.transform.right = new Vector3(PreyDir.x, PreyDir.y, 0f);
        atk = null;
        attacks[attackNum].canFire = false;
        yield return new WaitForSeconds(waitTime);

    }
    // Start is called before the first frame update
    void Start()
    {
        this.HP = this.MaxHP;   
        timer = peakTime;
        Prey[0] = GameObject.FindWithTag("MeleePlayer");
        Prey[1] = GameObject.FindWithTag("RangedPlayer");
        MeleeController= Prey[0].GetComponent<PlayerController>();
        RangedController = Prey[1].GetComponent<PlayerController>();
        MainCamController = MainCamera.GetComponent<CameraController>();
        isAttacking = false;
    }

    void Update()
    {   
        //Debug.Log(this.HP);
        //CoolDown for Bos Attack in action
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].cooldownTimer -= Time.deltaTime;
            //Allows Boss to attack again in x Seconds
            if (attacks[i].cooldownTimer <= i && attacks[i].canFire == false)
            {
                attacks[i].canFire = true;
                attacks[i].cooldownTimer = attacks[i].cooldown;
            }
        }

        if (colorTimer > 0)
        {
            colorTimer -= Time.deltaTime;
        }
        BossSprite.color = Color.Lerp(Color.white, Color.red, colorTimer / colorTime);
    }

    void FixedUpdate()
    {
        if (GameStats.isBattle)
        {
            
            /*if((2*(MaxHP/3)) > this.HP && this.HP >= (MaxHP/3))
            {
                Phase2Attack();
            }
            //else if(this.MaxHP/3 > this.HP && this.HP >= 1.00f)
            {
                Phase3Attack();
            }
            */
            if(this.HP < 1.00f)
                Destroy(gameObject);
            //Debug.Log(BossRB2D.velocity.magnitude);
            bossAnimator.SetFloat("speed", (Mathf.Abs(BossRB2D.velocity.magnitude)));
            if(BossRB2D.velocity.x < 0.0f)
                BossSprite.flipX = true;
            else if(BossRB2D.velocity.x > 0.0f)
                BossSprite.flipX = false;
            if(BossRB2D.velocity == Vector2.zero)
                bossAnimator.SetFloat("speed", 0.0f);

            //Making sure the player the boss is looking for is still in the game
            if ((MeleeController.isAlive || RangedController.isAlive) && !isAttacking)
            {
                visibleEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, lookRadius);
                if(visibleEnemies.Length != 0)
                {
                    for(int i = 0; i < visibleEnemies.Length; i++)
                    {
                        if ((visibleEnemies[i].gameObject.tag == "MeleePlayer") || (visibleEnemies[i].gameObject.tag == "RangedPlayer") && Vector2.Angle(BossRB2D.velocity, visibleEnemies[i].gameObject.transform.position) <= FOV) 
                        {
                            float distance = Vector2.Distance(visibleEnemies[i].gameObject.transform.position, BossRB2D.transform.position);
                            PreyDir = (visibleEnemies[i].gameObject.transform.position - BossRB2D.transform.position);
                            PreyDir.Normalize();
                            isAttacking = true;
                            //Knowing the direction and dist of the target if its clsoe enough launch an attakc in its dir
                            if (distance <= minDist && attacks[0].canFire == true)
                            {
                                BossSprite.enabled = false;
                                BossAudioSource.PlayOneShot(attacks[0].soundToPlay, 0.05f);
                                GameObject atk = PlayerController.Attack(attacks[0].atkObj, BossRB2D.transform.position, PreyDir, attacks[0].atkDistance, BossRB2D.transform.rotation);
                                atk = null;
                                Invoke("enableIdleSprite", 0.35f);
                                attacks[0].canFire = false;
                                
                            }
                            //if not then exit loop so we will check to see if the target is still in the scene
                            else if (distance > minDist && distance < maxDist && attacks[1].canFire == true)
                            {
                                StartCoroutine(StopBossAndWait(1.00f, 1));
                                BossRB2D.constraints = RigidbodyConstraints2D.None;
                                BossRB2D.velocity = prevVelocity;
                                //RotateBossToFace(BossRB2D.velocity);
                            }
                            else if (distance > maxDist && attacks[2].canFire == true)
                            {
                                StartCoroutine(StopBossAndWait(1.00f, 2));
                                BossRB2D.constraints = RigidbodyConstraints2D.None;
                                BossRB2D.velocity = prevVelocity;
                                //RotateBossToFace(BossRB2D.velocity);
                            }
                            isAttacking = false;
                        }
                        else
                            return;
                    }
                }
                
            }
 
        }
        else
            BossRB2D.isKinematic = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameStats.isBattle)
        {
            //Depenind on the type of attack it will  damage the boss on damage carried by its script and store damage info to the right player
            if (collision.collider.gameObject.tag == "Projectile")
            {
                PlayerShotController playerAttackController = collision.collider.gameObject.GetComponent<PlayerShotController>();
                this.HP -= playerAttackController.attack.damage;
                GameStats.RangedDamage += playerAttackController.attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, 0.05f);
                colorTimer = colorTime;
            }
            else if (collision.collider.gameObject.tag == "MeleeStrike")
            {
                PlayerStrikeController playerAttackController = collision.collider.gameObject.GetComponent<PlayerStrikeController>();
                if(playerAttackController.attack.fireKey == "Fire2")
                    MainCamController.timer = shakeTime;
                this.HP -= playerAttackController.attack.damage;
                GameStats.MeleeDamage += playerAttackController.attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, 0.05f);
                colorTimer = colorTime;


            }
        }
    }

    void Phase1Attack()
    {
        //Debug.Log("Im in Phase 1");
        bossCircleCollider.enabled = false;
        BossDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        BossDir.Normalize();
        BossRB2D.velocity = BossDir * (angularSpeed * Time.deltaTime);
        RotateBossToFace(BossRB2D.velocity);
    }

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
}

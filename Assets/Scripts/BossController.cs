using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;



public class BossController : MonoBehaviour
{

    public  string[] Preylabel = new string[2];
    public float BossVelocity;
    GameObject Prey;
    public float MaxHP;
    public float minDist, maxDist;
    public float angularSpeed;
    public AtkStruct[] attacks;
    public float peakTime;
    public AudioClip BossDamaged, BossDying;
    public PhysicsMaterial2D PureBounce;

    private float HP;
    private Rigidbody2D BossRB2D;
    private AudioSource BossAudioSource;
    private SpriteRenderer BossIdleSprite;
    private Vector3 BossDir;
    private Quaternion currentRotation;
    private float peak;
    private float timer;




    public float GetHP()
    {
        return this.HP;
    }

        void enableIdleSprite()
    {
        BossIdleSprite.enabled = true;
    }

    void Waiting()
    {

    }

    IEnumerator StopBossAndWait()
    {
        BossRB2D.velocity = Vector2.zero;
        BossRB2D.angularVelocity = 0.0f;
        yield return new WaitForSecondsRealtime(5);
    }
    // Start is called before the first frame update
    void Start()
    {
        BossRB2D = gameObject.GetComponent<Rigidbody2D>();
        BossIdleSprite = gameObject.GetComponent<SpriteRenderer>();
        BossAudioSource = gameObject.GetComponent<AudioSource>();
        Prey = GameObject.FindWithTag(Preylabel[0]);
        this.HP = this.MaxHP;   
        timer = peakTime;
        Phase1Attack();
        
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
        
    }

    void FixedUpdate()
    {


        if(BossRB2D.velocity == new Vector2(0.0f, 0.0f))
        {
            Phase1Attack();
        }

        /* 
        if((2*(MaxHP/3)) > this.HP && this.HP >= (MaxHP/3))
        {
            Phase2Attack();
        }
        else if(this.MaxHP/3 > this.HP && this.HP >= 1.00f)
        {
            Phase3Attack();
        }
        else if(this.HP < 1.00f)
        {
            Destroy(gameObject);
        }
        */
        

        //Boss rotates in the dir its moving.
        Quaternion Looking = Quaternion.identity;
        Looking.eulerAngles = new Vector3(0.0f, 0.0f,180-Mathf.Atan2(BossRB2D.velocity.x, BossRB2D.velocity.y) * Mathf.Rad2Deg);
        gameObject.transform.rotation = Looking;

        //Making sure the player the boss is looking for is still in the game
        if(Prey != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(BossRB2D.transform.position, BossRB2D.velocity, Mathf.Infinity);
            // Debug.DrawRay(BossRB2D.transform.position, BossRB2D.velocity*10, Color.red, 50.0f);
            if(hit.collider !=null)
            {
                
                BossRB2D.angularVelocity *= 0.0f;
                float distance = Vector2.Distance(BossRB2D.transform.position, Prey.transform.position);
 
                Vector3 PreyDir = (Prey.transform.position - BossRB2D.transform.position);
                Debug.Log(PreyDir);
                
                //Knowing the direction and dist of the target if its clsoe enough launch an attakc in its dir
                if( distance <= minDist && attacks[0].canFire == true)
                {
                    BossIdleSprite.enabled = false;
                    BossAudioSource.PlayOneShot(attacks[0].soundToPlay, 0.05f);
                    GameObject atk = PlayerController.Attack(attacks[0].atkObj, BossRB2D.transform.position, PreyDir, attacks[0].atkDistance, BossRB2D.transform.rotation);
                    atk = null;
                    Invoke("enableIdleSprite", 0.35f);
                    attacks[0].canFire = false; 
                    return;
                }

                //if not then exit loop so we will check to see if the target is still in the scene
                else if(distance > minDist && distance < maxDist && attacks[1].canFire == true)
                {
                    StartCoroutine(StopBossAndWait());
                    BossAudioSource.PlayOneShot(attacks[1].soundToPlay, 0.05f);
                    GameObject atk = PlayerController.Attack(attacks[1].atkObj, BossRB2D.transform.position, PreyDir, attacks[1].atkDistance, BossRB2D.transform.rotation);
                    atk = null;
                    attacks[1].canFire = false; 
                    StartCoroutine(StopBossAndWait()); 
                    return;

                }

                else if(distance >= maxDist && attacks[2].canFire == true)
                {
                    StartCoroutine(StopBossAndWait());
                    BossAudioSource.PlayOneShot(attacks[2].soundToPlay, 0.05f);
                    GameObject atk = PlayerController.Attack(attacks[2].atkObj, BossRB2D.transform.position, PreyDir, attacks[2].atkDistance, BossRB2D.transform.rotation);
                    atk = null;
                    attacks[2].canFire = false; 
                    StartCoroutine(StopBossAndWait());    
                    return;
                }
            }
            else
            {
                return;
            }
                        
        }
        //Once the first terget is down look for next target
        else
        {
            Prey = GameObject.FindWithTag(Preylabel[1]);
        }
        

     


    }


    void OnCollisionEnter2D(Collision2D collision)
    {
            //Depenind on the type of attack it will  damage the boss on damage carried by its script and store damage info to the right player
            if(collision.collider.gameObject.tag == "Projectile")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;
                GameStats.RangedDamage += collision.collider.gameObject.GetComponent<PlayerShotController>().attack.damage;

                BossAudioSource.PlayOneShot(BossDamaged, 0.05f);
            }
            else if(collision.collider.gameObject.tag == "MeleeStrike")
            {
                this.HP -= collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                GameStats.MeleeDamage += collision.collider.gameObject.GetComponent<PlayerStrikeController>().attack.damage;
                BossAudioSource.PlayOneShot(BossDamaged, 0.05f);
            }
    }

    void Phase1Attack()
    {
        Debug.Log("Im in Phase 1");
        gameObject.GetComponent<CircleCollider2D>().sharedMaterial = PureBounce;
        BossDir = new Vector3(Random.Range(-1.0f, 1.0f)+ 0.25f, Random.Range(-1.0f, 1.0f) + 0.25f, 1.0f);
        BossDir.Normalize();
        BossRB2D.velocity = BossDir * (angularSpeed*100) * Time.deltaTime;

    }

    void Phase2Attack()
    {
        gameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
        Debug.Log("Im in Phase 2");
        //trigmethod
        //Oscillation on one dimension = wave ocsillation where wave propogates in dimension T to oscillating dimension
        //Oscillation in 2-D
        //Obj oscillates on same trig func in both dimensions, obj oscillates between a start and end point on resulted vector
        //WHen Obj oscillates on both dimensions with using sin() and cos() respiectivelly results in circular oscillation 
        //where the diamater is equal to the peak in the oscillation shared by bnoth trig funcs
        //peak = 2.50f;
        //BossDir = new Vector3 (peak * Mathf.Cos(Time.time), peak * Mathf.Sin(Time.time), 1.00f);
        
        //modulusmethod
        
        //Oscillation is hard coded as Base of Value of timer @ some inst point in time mod Time it takes to reach crest/trough result is multiplied by velocity variable
        //This speed variable can help us control how much ground before x time until we go back in the opposite direction for the same amount of time.
    
        if(timer > 0.0f )
        {
            float oscillationWithModulus = (timer % peakTime) * angularSpeed;
            //Debug.Log(oscillationWithModulus);
            BossRB2D.velocity = new Vector3 (oscillationWithModulus, oscillationWithModulus , 0.0f);

        }
        else if(timer >= (-peakTime) && timer < 0.0f)
        {
            float oscillationWithModulus = ((-timer) % peakTime) * angularSpeed;
            //Debug.Log(oscillationWithModulus);
            BossRB2D.velocity = new Vector3 (oscillationWithModulus, -oscillationWithModulus, 0.0f);
        }
        else
        {
            timer = peakTime;
        }

        timer -= Time.deltaTime;

    }

    void Phase3Attack()
    {
        Debug.Log("Im in Phase 3");

    }
}

using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class PlayerController : MonoBehaviour  
{
    public float speed;
    public GameObject attack;
    public GameObject attackHvy;
    public float atkDistance;
    public float atkHvyDistance;
    public string vaxis;
    public string haxis;
    public string fireKey;
    public string fireKeyHvy;
    private Rigidbody2D MeleeRB2D;
    private Rigidbody2D RangedRB2D;
    private Transform pos;
    private Vector2 MoveDir;
    public float projectileCoolDown;
    public float atkHvyCooldown;
    private float timer;
    private float atkHvyTimer;
    public bool canFire;
    public bool canFireHvy;


    GameObject Attack(GameObject Atk, Vector3 atkPos, Quaternion ProjectileRotation)
    {
        GameObject atk = Instantiate(Atk, atkPos, pos.rotation);
        atk.transform.right = new Vector3(MoveDir.x, MoveDir.y, 0);
        return atk;
    }
   

    // Start is called before the first frame update
    void Start()
    {
        MeleeRB2D = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.GetComponent<Transform>();
        MoveDir = new Vector2(0, 0);
        timer = projectileCoolDown;
        atkHvyTimer = atkHvyCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (Input.GetAxis(haxis) != 0 || Input.GetAxis(vaxis) != 0)
        {
           
            //Movement
            MoveDir = new Vector2(Input.GetAxis(haxis), Input.GetAxis(vaxis));
            MoveDir.Normalize();
            MeleeRB2D.velocity = MoveDir * speed;
        }
        else
        {
            MeleeRB2D.velocity *= 0;
        }

        if (timer < 0 && canFire == false)
        {
            canFire = true;
            timer = projectileCoolDown;
        }
        timer -= Time.deltaTime;

        if (atkHvyTimer < 0 && canFireHvy == false)
        {
            canFireHvy = true;
            atkHvyTimer = atkHvyCooldown;
        }
        atkHvyTimer -= Time.deltaTime;

        // Attacking
        if (Input.GetKeyDown(fireKey))
        {
            Debug.Log(canFire);
            Vector3 atkPos = new Vector3(pos.position.x + MoveDir.x * atkDistance, pos.position.y + MoveDir.y * atkDistance, pos.position.z);
            if(attack.tag == "Projectile"  && canFire == true)
            {
                GameObject atk = Attack(attack, atkPos, pos.rotation);
                atk = null;
                canFire = false;
            }
            else if( attack.tag == "MeleeStrike")
            {
                GameObject atk = Attack(attack, atkPos, pos.rotation);
                atk = null;
            }
 
        }

        if (Input.GetKeyDown(fireKeyHvy))
        {
            Debug.Log(canFireHvy);
            Vector3 atkPos = new Vector3(pos.position.x + MoveDir.x * atkDistance, pos.position.y + MoveDir.y * atkDistance, pos.position.z);
            if (attackHvy.tag == "Projectile" && canFireHvy == true)
            {
                GameObject atk = Attack(attackHvy, atkPos, pos.rotation);
                atk = null;
                canFireHvy = false;
            }
            else if (attackHvy.tag == "MeleeStrike" && canFireHvy == true)
            {
                GameObject atk = Attack(attackHvy, atkPos, pos.rotation);
                atk = null;
                canFireHvy = false;
            }

        }
    }


}

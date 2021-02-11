using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class PlayerController : MonoBehaviour  
{
    public float speed;
    public GameObject attack;
    public float atkDistance;
    public string vaxis;
    public string haxis;
    public string fireKey;
    private Rigidbody2D PlayerRB2D;
    private Transform pos;
    private Vector2 MoveDir;
    public float projectileCoolDown;
    private float projectileTimer;
    public bool canFire;
    private GameObject GameView;
    private SpriteRenderer Player_Sprite;
    private int playerHP;



    GameObject Attack(GameObject Atk, Vector3 atkPos, Quaternion ProjectileRotation)
    {
        GameObject atk = Instantiate(attack, atkPos, pos.rotation);
        atk.transform.right = new Vector3(MoveDir.x, MoveDir.y, 0);
        return atk;
    }

    public int GetPlayerHP()
    {
        return playerHP;
    }

    public void ChangeHealth(int amnt, string operation)
    {
        if(operation == "+")
        {
            this.playerHP += amnt;
        }
        else if(operation == "-")
        {
            this.playerHP -= amnt;
        }
        if(this.playerHP>100)
        {
            this.playerHP = 100;
        }
    }
   

    // Start is called before the first frame update
    void Start()
    {
        PlayerRB2D = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.GetComponent<Transform>();
        MoveDir = new Vector2(0, 0);
        projectileTimer = projectileCoolDown;
        GameView = GameObject.FindWithTag("MainCamera");
        playerHP = 100;

    }

    void Update()
    {
        //Health Check
        if(playerHP <= 0)
        {
            Destroy(gameObject);
        }
    
        // Attacking
        if(Input.GetKeyDown(fireKey))
        {
           // Debug.Log(canFire);
            Vector3 atkPos = new Vector3(pos.position.x + MoveDir.x * atkDistance * Time.deltaTime, pos.position.y + MoveDir.y * atkDistance *Time.deltaTime, pos.position.z*Time.deltaTime);
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

    }

    // Update is called once per x frame
    void FixedUpdate()
    {

        //Movement
        if (Input.GetAxis(haxis) != 0 || Input.GetAxis(vaxis) != 0)
        {
            MoveDir = new Vector2(Input.GetAxis(haxis), Input.GetAxis(vaxis));
            MoveDir.Normalize();
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + ((Vector3)MoveDir * speed * Time.deltaTime));
        }
        else
        {
            PlayerRB2D.velocity *= 0;
        }

        if (projectileTimer < 0 && canFire == false)
        {
            canFire = true;
            projectileTimer = projectileCoolDown;
        }
        projectileTimer -= Time.deltaTime;
    }


}

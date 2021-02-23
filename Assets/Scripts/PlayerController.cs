using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class PlayerController : MonoBehaviour  
{
    public float speed;
    public AtkStruct[] attacks;
    public string vaxis;
    public string haxis;
   
    private Rigidbody2D PlayerRB2D;
    private Transform pos;
    private Vector2 MoveDir;

    private GameObject GameView;
    private SpriteRenderer Player_Sprite;
    private int playerHP;



    public static GameObject Attack(GameObject Atk, Vector3 atkPos, Quaternion ProjectileRotation)
    {
        GameObject atk = Instantiate(Atk, atkPos, ProjectileRotation);
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
        for (int i = 0; i < attacks.Length; i++)
            attacks[i].cooldownTimer = attacks[i].cooldown;
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
        for (int i = 0; i < attacks.Length; i++)
        {
            if (Input.GetKeyDown(attacks[i].fireKey) && attacks[i].canFire)
            {
                
                Vector3 atkPos = new Vector3(pos.position.x + MoveDir.x * attacks[i].atkDistance * Time.deltaTime, pos.position.y + MoveDir.y * attacks[i].atkDistance * Time.deltaTime, pos.position.z * Time.deltaTime);
                
                    GameObject atk = Attack(attacks[i].atkObj, atkPos, pos.rotation);
                    atk.transform.right = new Vector3(MoveDir.x, MoveDir.y, 0);
                    atk = null;
                attacks[i].canFire = false;
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
            PlayerRB2D.MovePosition(PlayerRB2D.transform.position + ((Vector3)MoveDir * speed * Time.deltaTime));
        }
        else
        {
            PlayerRB2D.velocity *= 0;
        }

        
        
    }


}

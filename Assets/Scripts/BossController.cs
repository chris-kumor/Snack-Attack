using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Rigidbody2D BossRB2D;
    public Camera BossLOS;
    public  string Preylabel;
    public float BossVelocity;
    GameObject Prey;
    public int HP;
    public int speed;
    public float angularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        BossRB2D = gameObject.GetComponent<Rigidbody2D>();
        Prey = GameObject.FindWithTag(Preylabel);
    
    }
    void Update()
    {
        
    }
    


    void FixedUpdate()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }

        if(Prey != null)
        {
            if(BossLOS.WorldToViewportPoint(Prey.transform.position).x > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).x < 1 &&BossLOS.WorldToViewportPoint(Prey.transform.position).y > 0 && BossLOS.WorldToViewportPoint(Prey.transform.position).y <1)
            {
                BossRB2D.angularVelocity = 0.0f;
                float distance = Vector2.Distance(BossRB2D.transform.position, Prey.transform.position);
                if( distance > 2.00f)
                {
                    BossRB2D.transform.position = Vector2.Lerp(BossRB2D.transform.position, Prey.transform.position, (BossVelocity * Time.deltaTime));
                }
                else
                {
                    BossRB2D.velocity = new Vector2(0.0f, 0.0f);
                    BossRB2D.angularVelocity = 0.0f;
                }
            }
            else
            {
                BossRB2D.velocity = new Vector2(0.0f, 0.0f);
                Vector3 preyDirection =  Prey.transform.position -  gameObject.transform.position; 
                float singleStep = angularSpeed * Time.deltaTime;
                Vector3 lookDir = Vector3.RotateTowards(gameObject.transform.position, preyDirection, singleStep, 0.0f);
                BossRB2D.MoveRotation(Quaternion.LookRotation(lookDir, Vector3.forward));
            }
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Projectile" || collision.collider.tag == "MeleeStrike")
        {
            this.HP -= 1;
            Debug.Log("The Bosses health is now " + this.HP + ".");
        }
        else if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11 )
        {
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(5, "-");
            Debug.Log(collision.gameObject.name + " has " + collision.gameObject.GetComponent<PlayerController>().GetPlayerHP() + " health.");
        }

    }
}

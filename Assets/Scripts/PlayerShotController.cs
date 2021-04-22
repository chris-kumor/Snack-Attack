using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public string PlayerTag;
    public AtkStruct attack;

    private Vector2 angle;
    private float spriteAngle;
    private float angleDif;
    private GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag(PlayerTag);
        angle = new Vector2(rb.transform.right.x, rb.transform.right.y);
        angleDif = Random.Range(-10, 10);
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(PlayerTag == "RangedPlayer" || PlayerTag == "Boss")
            Player.SendMessage("CanAttack");
        Destroy(gameObject);
    }


    void FixedUpdate()
    {
        rb.velocity = angle * speed * Time.deltaTime;
        rb.transform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
        spriteAngle += angleDif;
    }
}

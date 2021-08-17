using System.Collections;
using System.Collections.Generic;
using UnityEngine;public class PlayerShotController : MonoBehaviour{
    public float speed;
    public Rigidbody2D rb;
    public AtkStruct attack;
    public string playerTag;
    private Vector2 angle;
    private float spriteAngle;
    private float angleDif;
    private GameObject Player;
    // Start is called before the first frame update
    void Start(){   
        Player = GameObject.FindWithTag(playerTag);
        angle = new Vector2(rb.transform.right.x, rb.transform.right.y);
        angleDif = Random.Range(-10, 10);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject != null){
            if(Player != null){
                if(Player.tag == "RangedPlayer" || Player.tag == "Boss")
                    Player.SendMessage("CanAttack");
            }
        }
        Destroy(gameObject);
    }
    void FixedUpdate(){
        rb.velocity = angle * speed * Time.deltaTime;
        rb.transform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
        spriteAngle += angleDif;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackCOntroller;

public class AppleMinnionController : MonoBehaviour
{
    private Vector3 PreyDir;
    private GameObject[] targets = new GameObject[2];
    private GameObject currentTarget;
    
    
    
    public Rigidbody2D minnionRB;
    public float minionSpeed;
    public AtkStruct attack;
    public SpriteRenderer AppleMinnionSprite;
    

    public Vector2 GetPreyDir(){
        return PreyDir;
    }
    
    // Start is called before the first frame update
    void Start(){
        targets[0] = GameObject.FindWithTag("MeleePlayer");
        targets[1] = GameObject.FindWithTag("RangedPlayer");
        if(Vector2.Distance(targets[0].transform.position, gameObject.transform.position) < Vector2.Distance(targets[1].transform.position, gameObject.transform.position))
            currentTarget = targets[0];
         else
            currentTarget = targets[1];
        minnionRB = gameObject.GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate(){
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentTarget.transform.position, (minionSpeed * Time.deltaTime));
        if(minnionRB.velocity.x > 0.0f)
            AppleMinnionSprite.flipX = true;
        else if(minnionRB.velocity.x < 0.0f)
            AppleMinnionSprite.flipX = false;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject != null){
            PreyDir = minnionRB.velocity;
            PreyDir.Normalize();
            GameStats.minions -= 1;
            GameObject atk = AttackCOntroller.Attack(attack.atkObj, gameObject.transform.position, PreyDir, attack.atkDistance, gameObject.transform.rotation);
            atk = null;
            Destroy(gameObject);
            
        }
    }
}

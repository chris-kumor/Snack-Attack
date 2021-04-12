using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class AppleMinionCOntroller : MonoBehaviour
{

    private Vector3 PreyDir;
    public Rigidbody2D rigidbody;
    private GameObject currentTarget;
    
    public float minionSpeed;
    public AtkStruct[] attacks;
    public SpriteRenderer AppleMinnionSprite;
    public GameObject[] targets = new GameObject[2];

    
    // Start is called before the first frame update
    void Start()
    {
        if(Vector2.Distance(gameObject.transform.position, targets[0].transform.position) < Vector2.Distance(gameObject.transform.position, targets[1].transform.position))
            currentTarget = targets[0];
         else
            currentTarget = targets[1];
    }

    
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentTarget.transform.position, (minionSpeed * Time.deltaTime));
        if(rigidbody.velocity.x > 0.0f)
            AppleMinnionSprite.flipX = true;
        else if(rigidbody.velocity.x < 0.0f)
            AppleMinnionSprite.flipX = false;

        
        
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PreyDir = rigidbody.velocity;
        PreyDir.Normalize();
        if(collision.gameObject.tag == "MeleeStrike")
            Destroy(gameObject);
        else
        {
            GameObject atk = PlayerController.Attack(attacks[0].atkObj, gameObject.transform.position, PreyDir, attacks[0].atkDistance, gameObject.transform.rotation);
            atk = null;
            Destroy(gameObject);
        }
    }
}

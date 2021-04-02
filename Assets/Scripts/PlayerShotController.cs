using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float timer;
    public float speed;
    
    public AtkStruct attack;
    private Vector2 angle;
    private Rigidbody2D rb;
    private Transform pos;
    private float spriteAngle;
    private float angleDif;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.GetComponent<Transform>();
        angle = new Vector2(pos.right.x, pos.right.y);
        angleDif = Random.Range(-10, 10);
    }

    void Update()
    {
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }


    void FixedUpdate()
    {

        rb.velocity = angle * speed * Time.deltaTime;
        pos.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
        spriteAngle += angleDif;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

 
}

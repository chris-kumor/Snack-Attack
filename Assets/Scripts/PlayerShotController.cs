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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.GetComponent<Transform>();
        angle = new Vector2(pos.right.x, pos.right.y);
    }

    void Update()
    {
        
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }


    void FixedUpdate()
    {

        rb.velocity = angle * speed * Time.deltaTime;
        rb.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);     

    }

 
}

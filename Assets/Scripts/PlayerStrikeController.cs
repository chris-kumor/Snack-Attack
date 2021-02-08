using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeController : MonoBehaviour
{
    public float timer = 0.0001f;
    public Vector2 angle;
    private Rigidbody2D rb;
    private Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = gameObject.GetComponent<Transform>();
        angle = new Vector2(pos.right.x, pos.right.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeController : MonoBehaviour
{
    public float timer;
    public AtkStruct attack;
    public SpriteRenderer AttackSprite;
    
    private PolygonCollider2D AttkCollider;
    private GameObject MeleePlayer;
    private GameObject RangedPlayer;
    private Rigidbody2D rb;
    
    


    // Start is called before the first frame update
    void Start()
    {

        AttkCollider = gameObject.GetComponent<PolygonCollider2D>();
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    void Update()
    {
        Vector2 AimDir =  MeleePlayer.GetComponent<PlayerController>().GetAimDir();
        if(AimDir.x < 0.0f && AimDir.y != 0.0f && gameObject.tag == "MeleeStrike")
        {
            AttackSprite.flipY = true;
        }
        else
        {
            AttackSprite.flipY = false;
        }
        
        if(gameObject.tag == "Shield" && AttkCollider.bounds.Contains(MeleePlayer.transform.position))
        {
            gameObject.transform.position = MeleePlayer.transform.position;
        }
        if(gameObject.tag == "Shield" && AttkCollider.bounds.Contains(RangedPlayer.transform.position))
        {
            gameObject.transform.position = RangedPlayer.transform.position;
        }
         //rb.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward); 
    }
}

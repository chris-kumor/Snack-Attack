using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class AppleMinionCOntroller : MonoBehaviour
{

    private Vector3 PreyDir;
    public Rigidbody2D rigidbody;
    private GameObject currentTarget, Map;
    
    public float minionSpeed;
    public AtkStruct attack;
    public SpriteRenderer AppleMinnionSprite;
    private GameObject[] targets = new GameObject[2];
    private SpawnMinion spawnMinionController;

    public GameObject atkObj;

    public Vector3 GetPreyDir()
    {
        return PreyDir;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        Map = GameObject.FindWithTag("Map");
        targets[0] = GameObject.FindWithTag("MeleePlayer");
        targets[1] = GameObject.FindWithTag("RangedPlayer");
        spawnMinionController = Map.GetComponent<SpawnMinion>();
        if(Vector2.Distance(targets[0].transform.position, gameObject.transform.position) < Vector2.Distance(targets[1].transform.position, gameObject.transform.position))
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
        if(collision.gameObject != null)
        {
            PreyDir = rigidbody.velocity;
            PreyDir.Normalize();
            spawnMinionController.minions -= 1;
            if(collision.gameObject.tag == "MeleeStrike")
                Destroy(gameObject);
            else
            {
                GameObject atk = PlayerController.Attack(atkObj, gameObject.transform.position, PreyDir, attack.atkDistance, gameObject.transform.rotation);
                atk = null;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class MeleePlayerController : MonoBehaviour  
{
    public float speed;
    public GameObject attack;
    public float atkDistance = 1;
    public string vaxis = "Vertical";
    public string haxis = "Horizontal";
    public string fireKey = "space";
    private Rigidbody2D MeleeRB2D;
    private Transform pos;
    private Animation MeleePlayerAnimation;
    private Vector2 MoveDir;
   

    // Start is called before the first frame update
    void Start()
    {
        MeleeRB2D = gameObject.GetComponent<Rigidbody2D>();
        MeleePlayerAnimation = gameObject.GetComponent<Animation>();
        pos = gameObject.GetComponent<Transform>();

        MoveDir = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        MeleePlayerAnimation.Play("Idle", PlayMode.StopSameLayer);
        if (Input.GetAxis(haxis) != 0 || Input.GetAxis(vaxis) != 0)
        {
            //MeleePlayerAnimation.CrossFade("Run",0.3f, PlayMode.StopSameLayer);
            //Movement

            MoveDir = new Vector2(Input.GetAxis(haxis), Input.GetAxis(vaxis));
            MoveDir.Normalize();
            MeleeRB2D.velocity = MoveDir * speed;
        }
        else
        {
            MeleeRB2D.velocity *= 0;
            MeleePlayerAnimation.CrossFade("Idle", 0.3f, PlayMode.StopSameLayer);
        }

        // Attacking
        if(Input.GetKeyDown(fireKey))
        {
            Vector3 atkPos = new Vector3(pos.position.x + MoveDir.x * atkDistance, pos.position.y + MoveDir.y * atkDistance, pos.position.z);
            GameObject atk = Instantiate(attack, atkPos, pos.rotation);
            atk.transform.right = new Vector3(MoveDir.x, MoveDir.y, 0);
            atk = null;
        }
        
    }


}

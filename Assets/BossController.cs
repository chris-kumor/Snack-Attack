using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Rigidbody2D BossRB2D;
    public GameObject BossLOS;
    public GameObject BossLOSDir;
    public  GameObject Prey;
    public float angular_speed;
    Vector3 screenPoint;
    public float BossVelocity;



    // Start is called before the first frame update
    void Start()
    {
        //BossLOS = gameObject.GetComponent<Physics2DRaycaster>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* RaycastHit2D
        if(BossLOS.collider != null)
        {
            if(BossLOS.collider.layer == 9)
            {
                gameObject.getComponent<Rigidbody2D>().AddForce(hit.gameObject.transform.position, ForceMode.Force);
            }
        }
        */
    }
}

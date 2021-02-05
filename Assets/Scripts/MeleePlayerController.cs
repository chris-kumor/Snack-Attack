using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class MeleePlayerController : MonoBehaviour  
{
    public float speed;
    private Rigidbody2D MeleeRB2D;
    private Animation MeleePlayerAnimation;
   

    // Start is called before the first frame update
    void Start()
    {
        MeleeRB2D = gameObject.GetComponent<Rigidbody2D>();
        MeleePlayerAnimation = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        MeleePlayerAnimation.Play("Idle", PlayMode.StopSameLayer);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //MeleePlayerAnimation.CrossFade("Run",0.3f, PlayMode.StopSameLayer);
            //Movement
            if(Input.GetAxis("Vertical") > 0)
            {
                if(Input.GetAxis("Horizontal") < 0)
                {
                    MeleeRB2D.AddForce(new Vector2(-speed, speed));
                }
                else if(Input.GetAxis("Horizontal") > 0)
                {
                    MeleeRB2D.AddForce(new Vector2(speed, speed));
                }
                else
                {
                    MeleeRB2D.AddForce(new Vector2(0.0f, speed));
                }
            
                
            }
            else if(Input.GetAxis("Vertical") < 0)
            {
                if(Input.GetAxis("Horizontal") < 0)
                {
                    MeleeRB2D.AddForce(new Vector2(-speed, -speed));
                }
                if(Input.GetAxis("Horizontal") > 0)
                {
                    MeleeRB2D.AddForce(new Vector2(speed, -speed));
                }
                else
                {
                    MeleeRB2D.AddForce(new Vector2(0.0f, -speed));
                }
            }
            else if ( Input.GetKey(KeyCode.A))
            {
                    MeleeRB2D.AddForce(new Vector2(-speed, 0.0f));
            }

            else if ( Input.GetKey(KeyCode.D))
            {
                MeleeRB2D.AddForce( new Vector2(speed, 0.0f));
            }
            
        }
        MeleeRB2D.velocity = new Vector2(0.0f, 0.0f);
        MeleePlayerAnimation.CrossFade("Idle",0.3f, PlayMode.StopSameLayer);




        

    }


}

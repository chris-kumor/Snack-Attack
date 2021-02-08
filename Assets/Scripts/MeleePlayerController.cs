using System.Collections;
using System.Collections.Generic; 
using UnityEngine;




public class MeleePlayerController : MonoBehaviour  
{
    public float speed;
    private Rigidbody2D MeleeRB2D;
    private Animation MeleePlayerAnimation;
    Vector3 angle;
   

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
        //MeleePlayerAnimation.CrossFade("Run",0.3f, PlayMode.StopSameLayer);
        //Movement

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            angle.x = Input.GetAxis("Horizontal");
            angle.z =  0;
            angle.y = Input.GetAxis("Vertical");
            MeleeRB2D.MovePosition(MeleeRB2D.transform.position + (angle * speed));
        }
        
        

        /*if(Input.GetAxis("Vertical") > 0)
        {
            if(Input.GetAxis("Horizontal") < 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(-speed, speed, 0.0f));
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, speed, 0.0f));
            }
            else
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(0.0f, speed, 0.0f));
            }
        }

        else if(Input.GetAxis("Vertical") < 0)
        {
            if(Input.GetAxis("Horizontal") < 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(-speed, -speed, 0.0f));
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, -speed, 0.0f));
            }
            else
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(0.0f, -speed, 0.0f));
            }
            
        }  

        else if(Input.GetAxis("Horizontal") < 0)
        {
            if(Input.GetAxis("Vertical") < 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(-speed, -speed, 0.0f));
            }
            else if(Input.GetAxis("Vertical") > 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, speed, 0.0f));
            }
            else
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(-speed, 0.0f, 0.0f));
            }
        }

        else if(Input.GetAxis("Horizontal") > 0)
        {
            if(Input.GetAxis("Vertical") < 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, -speed, 0.0f));
            }

            else if(Input.GetAxis("Vertical") > 0)
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, speed, 0.0f));
            }
            else
            {
                MeleeRB2D.MovePosition(MeleeRB2D.transform.position + new Vector3(speed, 0.0f, 0.0f));
            }
        }*/

        

        MeleeRB2D.velocity = new Vector2(0.0f, 0.0f);
        //MeleePlayerAnimation.CrossFade("Idle",0.3f, PlayMode.StopSameLayer);




        

    }


}

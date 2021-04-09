using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
 
    
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    

    private float currentDashTime = maxDashTime;
    private float dashSpeed = 6;
    private Rigidbody2D PlayerRB2D;
    private Vector3 moveDirection;
    private SinputSystems.InputDeviceSlot slot; 
 
     
 
     public void Awake()
     {
         PlayerRB2D = GetComponent<Rigidbody2D>();
         if(gameObject.tag == "MeleePlayer")
         {
             slot = GameStats.MeleeSlot;
         }
         else
         {
             slot = GameStats.RangedSlot;
         }
     }
 
     // Update is called once per frame
     void Update () {

         Vector2 AimDir = gameObject.GetComponent<PlayerController>().GetAimDir();
         //AimDir.Normalize();

         if (Sinput.GetButtonDown("Dash", slot)) 
         {
             //Debug.Log("I want to dash!");
             currentDashTime = 0;                
         }
         if(currentDashTime < maxDashTime)
         {
             moveDirection = new Vector3(AimDir.x * dashDistance, AimDir.y * dashDistance, 1.00f);
             currentDashTime += dashStoppingSpeed;
         }
         else
         {
             moveDirection = Vector3.zero;
         }
         PlayerRB2D.AddForce(((Vector2)PlayerRB2D.transform.position + (Vector2)moveDirection * Time.deltaTime * dashSpeed), ForceMode2D.Force);
     }
}

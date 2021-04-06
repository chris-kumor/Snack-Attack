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
         if (Sinput.GetButton("Dash", slot)) //Right mouse button
         {
             //Debug.Log("I want to dash!");
             currentDashTime = 0;                
         }
         if(currentDashTime < maxDashTime)
         {
             moveDirection = transform.position * dashDistance;
             currentDashTime += dashStoppingSpeed;
         }
         else
         {
             moveDirection = Vector3.zero;
         }
         PlayerRB2D.MovePosition(PlayerRB2D.transform.position + moveDirection * Time.deltaTime * dashSpeed);
     }
}

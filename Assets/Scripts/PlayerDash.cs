using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
 
    
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    public string DashButton;

    private float currentDashTime = maxDashTime;
    private float dashSpeed = 6;
    private Rigidbody2D PlayerRB2D;
    private Vector3 moveDirection;
 
     
 
     private void Awake()
     {
         PlayerRB2D = GetComponent<Rigidbody2D>();
     }
 
     // Update is called once per frame
     void Update () {
         if (Input.GetButtonDown(DashButton)) //Right mouse button
         {
             currentDashTime = 0;                
         }
         if(currentDashTime < maxDashTime)
         {
             moveDirection = transform.forward * dashDistance;
             currentDashTime += dashStoppingSpeed;
         }
         else
         {
             moveDirection = Vector3.zero;
         }
         PlayerRB2D.MovePosition(PlayerRB2D.transform.position + moveDirection * Time.deltaTime * dashSpeed);
     }
}

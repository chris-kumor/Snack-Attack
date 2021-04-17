using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
 
    
    public  float maxDashTime;
    public float dashDistance;
    public float dashStoppingSpeed;
    

    private float currentDashTime;
    private float dashSpeed = 6;
    private Rigidbody2D PlayerRB2D;
    private Vector2 moveDirection;
    private SinputSystems.InputDeviceSlot slot; 
    private Vector2 AimDir;

    private bool isMelee, useDash;

    private PlayerController playerController;

 
     
 
     public void Start()
     {
         
         if(gameObject.tag == "MeleePlayer")
            isMelee = true;
        else
            isMelee = false;
         PlayerRB2D = GetComponent<Rigidbody2D>();
         playerController = GetComponent<PlayerController>();

         if(isMelee)
             slot = GameStats.MeleeSlot;
         else 
             slot = GameStats.RangedSlot; 
        currentDashTime = maxDashTime;

     }

    public void isDashing()
    {
        if(GameStats.bothPlayersKB)
        {
            if(isMelee)
                useDash = Sinput.GetButtonDown("Dash", slot);
            else
                useDash = Sinput.GetButtonDown("AltRDash", slot);
        }
    }

 
     // Update is called once per frame
     void Update () 
     {        
         //AimDir.Normalize();
        
        isDashing();

        if (useDash && !playerController.isDashing)
        {   
             currentDashTime = 0;
             playerController.isDashing = true;
        }           

        if(currentDashTime <= maxDashTime && playerController.isAlive && !playerController.attackStatus())
        {
             AimDir = playerController.GetAimDir();
             //Debug.Log(AimDir);
             moveDirection = new Vector2(AimDir.x * dashDistance, AimDir.y * dashDistance);
             currentDashTime += dashStoppingSpeed;
             PlayerRB2D.AddForceAtPosition(moveDirection, PlayerRB2D.position, ForceMode2D.Force);
         }
         else
         {
             moveDirection = Vector3.zero;
             playerController.isDashing = false;
         }
            
     }
}

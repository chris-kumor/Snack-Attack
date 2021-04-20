using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
 
    
    public  float maxDashTime;
    public float dashDistance;
    public float dashStoppingSpeed;
    public ParticleSystem DashSprint, DashTrail;
    public GameObject AimSprite, DashParticles;
    

    private float currentDashTime;
    //private float dashSpeed = 6;
    private Rigidbody2D PlayerRB2D;
    private Vector2 moveDirection;
    private SinputSystems.InputDeviceSlot slot; 
    private Vector2 AimDir;

    private bool isMelee, useDash;

    private PlayerController playerController;

    private RectTransform dashTransform;

 
     
 
     public void Start()
     {
         
         if(gameObject.tag == "MeleePlayer")
            isMelee = true;
        else
            isMelee = false;
         PlayerRB2D = GetComponent<Rigidbody2D>();
         playerController = GetComponent<PlayerController>();
         dashTransform = DashParticles.GetComponent<RectTransform>();
         if(isMelee)
             slot = GameStats.MeleeSlot;
         else 
             slot = GameStats.RangedSlot; 
        currentDashTime = maxDashTime;
        DashSprint.Clear();
        DashTrail.Clear();

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
        else if(!GameStats.bothPlayersKB)
            useDash = Sinput.GetButtonDown("Dash", slot);
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
             DashSprint.Play();
             DashTrail.Play();
        }           

        if(currentDashTime <= maxDashTime && playerController.isAlive && !playerController.attackStatus())
        {
             AimDir = playerController.GetAimDir();
             Quaternion dashParticleDir = new Quaternion(0.0f, 0.0f, AimSprite.transform.rotation.z + 180.0f, 0.0f);
             dashTransform.rotation = dashParticleDir;
             //Debug.Log(AimDir);
             moveDirection = new Vector2(AimDir.x * dashDistance, AimDir.y * dashDistance);
             currentDashTime += dashStoppingSpeed;
             PlayerRB2D.AddForceAtPosition(moveDirection, PlayerRB2D.position, ForceMode2D.Force);
         }
         else
         {
             moveDirection = Vector3.zero;
             playerController.isDashing = false;
             DashSprint.Stop();
             DashTrail.Stop();

         }
            
     }
}

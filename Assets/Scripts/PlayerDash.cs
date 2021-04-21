using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
 
    
    public  float maxDashTime;
    public float dashDistance;
    public float dashStoppingSpeed;
    
    

    private float currentDashTime;
    //private float dashSpeed = 6;
    private Rigidbody2D PlayerRB2D;
    private Vector2 moveDirection;
    private SinputSystems.InputDeviceSlot slot; 
    private Vector2 AimDir;

    private bool isMelee, useDash;
    private PlayerController playerController;
    public ParticleSystem  dashPartSprint, dashPartTrail;
    public RectTransform dashEffectTransform;
    public Rigidbody2D AimSpriteRB2D;

 
     
 
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
        dashPartSprint.Clear();
        dashPartTrail.Clear();

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
             dashPartSprint.Play();
             dashPartTrail.Play();
        }           

        if(currentDashTime <= maxDashTime && playerController.isAlive && !playerController.attackStatus())
        {
             AimDir = playerController.GetAimDir();
             //Debug.Log(AimDir);
             moveDirection = new Vector2(AimDir.x * dashDistance, AimDir.y * dashDistance);
             currentDashTime += dashStoppingSpeed;
             Debug.Log(AimSpriteRB2D.rotation);
             dashEffectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, AimSpriteRB2D.rotation-90.0f);
             ParticleSystemRenderer DashPartRendSprite = dashPartSprint.GetComponent<ParticleSystemRenderer>();
             if(AimDir.x < 0.0f)
             {
                 DashPartRendSprite.flip = new Vector3(1.0f, 0.0f, 0.0f);
             }
             else if(AimDir.x > 0.0f)
             {
                DashPartRendSprite.flip= new Vector3(0.0f, 0.0f, 0.0f);
             }
             PlayerRB2D.AddForceAtPosition(moveDirection, PlayerRB2D.position, ForceMode2D.Force);
         }
         else
         {
             moveDirection = Vector3.zero;
             playerController.isDashing = false;
             dashPartSprint.Stop();
             dashPartTrail.Stop();

         }
            
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeController : MonoBehaviour
{
    private float timer;
    public AtkStruct attack;
    public SpriteRenderer AttackSprite;
    public string ParentTag;

    private GameObject Parent;

    private Vector2 AimDir;

    
    
    


    // Start is called before the first frame update
    void Start()
    {
        timer = attack.cooldown;
        Parent = GameObject.FindWithTag(ParentTag);
        if(ParentTag == "MeleePlayer")
            AimDir = Parent.GetComponent<PlayerController>().GetAimDir();
        else if(ParentTag == "Boss")
            AimDir = Parent.GetComponent<BossController>().GetPreyDir();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0)
        {   
            if(ParentTag == "MeleePlayer")
                Parent.SendMessage("CanAttack");
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    void Update()
    {
            if(AimDir.x < 0.0f && AimDir.y != 0.0f && gameObject.tag == "MeleeStrike")
                AttackSprite.flipY = true;
            else
                AttackSprite.flipY = false;
        
    }

}

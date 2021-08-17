using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeController : MonoBehaviour{
    private float timer;
    public AtkStruct attack;
    public SpriteRenderer AttackSprite;
    private GameObject Parent;
    private Vector2 AimDir;
    public string parentTag;
    // Start is called before the first frame update
    void Start(){
        Parent = GameObject.FindWithTag(parentTag);
        timer = attack.cooldown;
        //Parent = GameObject.FindWithTag(ParentTag);
        if(Parent.tag == "MeleePlayer")
            AimDir = Parent.GetComponent<PlayerController>().GetAimDir();
        else if(Parent.tag == "Boss")
            AimDir = Parent.GetComponent<BossController>().GetPreyDir();
        else if(Parent.tag == "minion")
            AimDir = (Vector2)Parent.GetComponent<AppleMinnionController>().GetPreyDir();
    }
    // Update is called once per frame
    void FixedUpdate(){
        if (timer <= 0)
        {   
            if(Parent != null)
            {
                if((Parent.tag == "MeleePlayer" || Parent.tag == "Boss"))
                {
                    if(Parent.tag == "Boss")
                    {
                        Parent.SendMessage("UnFreezeBoss");
                        Parent.SendMessage("enableIdleSprite");
                    }
                    Parent.SendMessage("CanAttack");
                }
            }
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
    void Update(){
            if(AimDir.x < 0.0f && AimDir.y != 0.0f)
                AttackSprite.flipY = true;
            else               
                AttackSprite.flipY = false;  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour{
    // Start is called before the first frame update
    private GameObject MeleePlayer, RangedPlayer, MReviveIcon, RReviveIcon;
    public Text ReviveStatus;
    private PlayerController rangedController, meleeController;
    public void findRanged(){
        
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        RReviveIcon = RangedPlayer.transform.Find("ReviveSprite").gameObject;
        
    }
    public void findMelee(){
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        meleeController = MeleePlayer.GetComponent<PlayerController>();
        MReviveIcon = MeleePlayer.transform.Find("ReviveSprite").gameObject;
    }    
    void Start(){
        ReviveStatus.enabled = false;
    }
    // Update is called once per frame
    void Update(){
        if(!rangedController.isAlive)
            RReviveIcon.SetActive(true);
        else if(rangedController.isAlive)
            RReviveIcon.SetActive(false);
        if(!meleeController.isAlive)
            MReviveIcon.SetActive(true);
        else if(meleeController.isAlive)
            MReviveIcon.SetActive(false);
        if(!rangedController.isAlive || !meleeController.isAlive)
            ReviveStatus.enabled = true;
        else if(rangedController.isAlive && meleeController.isAlive)
             ReviveStatus.enabled = false;
    }
}

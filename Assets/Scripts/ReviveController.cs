using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour{
    // Start is called before the first frame update
    public GameObject MeleePlayer, RangedPlayer, MReviveIcon, RReviveIcon;
    public Text ReviveStatus;
    public PlayerController rangedController, meleeController;
    public void findRanged(){
        Debug.Log("Looking for Ranged Player in Revive COntroller");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        RReviveIcon = GameObject.FindWithTag("RReviveIcon");
    }
    public void findMelee(){
        Debug.Log("Looking for melee player in revive controller");
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        meleeController = MeleePlayer.GetComponent<PlayerController>();
        MReviveIcon = GameObject.FindWithTag("MReviveIcon");
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

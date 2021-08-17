using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour{
    // Start is called before the first frame update
    private GameObject MeleePlayer, RangedPlayer, MReviveIcon, RReviveIcon;
    public Text ReviveStatus;
    private PlayerController rangedController, meleeController;
    public void findPlayers(){
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        meleeController = MeleePlayer.GetComponent<PlayerController>();
    }
    void Start(){
        findPlayers();
        MReviveIcon.SetActive(false);
        RReviveIcon.SetActive(false);
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

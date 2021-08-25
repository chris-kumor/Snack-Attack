using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMeleeHealth : MonoBehaviour{
    public Image MeleePlayerHealthBar;
    public GameObject MeleePlayer;
    private PlayerController meleeController;
    public void findMelee(){
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        meleeController = MeleePlayer.GetComponent<PlayerController>();
        MeleePlayerHealthBar.fillAmount = 1;
    }
    // Update is called once per frame
    void Update(){
        if(MeleePlayer != null)
            MeleePlayerHealthBar.fillAmount = (meleeController.playerHP/meleeController.MaxHP);
    }
}

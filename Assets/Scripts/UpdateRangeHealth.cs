using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRangeHealth : MonoBehaviour{
    public Image RangedPlayerHealthBar;
    private GameObject RangedPlayer;
    private PlayerController rangedController;
 
    // Start is called before the first frame update
    public void findRanged(){
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        RangedPlayerHealthBar.fillAmount = 1;
    }
    // Update is called once per frame
    void Update(){
        if(RangedPlayer != null)
            RangedPlayerHealthBar.fillAmount = (rangedController.playerHP/rangedController.MaxHP);
    }
}

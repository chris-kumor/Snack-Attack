using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRangeHealth : MonoBehaviour{
    public Image RangedPlayerHealthBar;
    public GameObject RangedPlayer;
    private PlayerController rangedController;
 
    // Start is called before the first frame update
    void Start(){
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        
    }
    // Update is called once per frame
    void Update(){
        if(RangedPlayer != null)
            RangedPlayerHealthBar.fillAmount = (rangedController.playerHP/rangedController.MaxHP);
    }
}

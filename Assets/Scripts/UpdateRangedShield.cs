using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateRangedShield : MonoBehaviour{
    private Image rangedShieldUI;
    private GameObject RangedShield;
    private AtkStruct shield;
    // Start is called before the first frame update
    public void findRShield(){
        RangedShield = GameObject.FindWithTag("RangedShield");
        rangedShieldUI = gameObject.GetComponent<Image>();
        shield = RangedShield.GetComponent<ShieldController>().shield;
        rangedShieldUI.fillAmount = 1;
    }
    // Update is called once per frame
    void Update(){
        if(RangedShield != null)
            rangedShieldUI.fillAmount = shield.cooldownTimer/shield.cooldown;
    }

}

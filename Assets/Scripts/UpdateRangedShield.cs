using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateRangedShield : MonoBehaviour
{
    private Image rangedShieldUI;
    private GameObject RangedShield;
    private AtkStruct shield;
    // Start is called before the first frame update
    void Start()
    {
        RangedShield = GameObject.FindWithTag("RangedShield");
        rangedShieldUI = gameObject.GetComponent<Image>();
        shield = RangedShield.GetComponent<ShieldController>().shield;
    }

    // Update is called once per frame
    void Update()
    {
        if(RangedShield != null)
            rangedShieldUI.fillAmount = shield.cooldownTimer/shield.cooldown;
    }

}

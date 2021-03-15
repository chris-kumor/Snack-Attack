using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateRangedShield : MonoBehaviour
{
    private Image rangedShieldUI;
    public GameObject RangedShield;
    // Start is called before the first frame update
    void Start()
    {
        rangedShieldUI = gameObject.GetComponent<Image>();
        rangedShieldUI.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(RangedShield != null)
        {
            rangedShieldUI.fillAmount = RangedShield.GetComponent<ShieldController>().shield.cooldownTimer/RangedShield.GetComponent<ShieldController>().shield.cooldown;
        }
    }

}

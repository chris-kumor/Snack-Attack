using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMeleeShield : MonoBehaviour
{
    private Image meleeShieldUI;
    public GameObject MeleeShield;
    // Start is called before the first frame update
    void Start()
    {
        meleeShieldUI=gameObject.GetComponent<Image>();
        meleeShieldUI.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(MeleeShield != null)
        {
            meleeShieldUI.fillAmount = MeleeShield.GetComponent<ShieldController>().shield.cooldownTimer/MeleeShield.GetComponent<ShieldController>().shield.cooldown;
        }
    }
}

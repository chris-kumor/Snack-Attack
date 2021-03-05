using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBossHealth : MonoBehaviour
{
    private Image BossHealthBar;
    private GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        BossHealthBar = gameObject.GetComponent<Image>();
        Boss = GameObject.FindWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss != null)
        {
            BossHealthBar.fillAmount = (Boss.GetComponent<BossController>().GetHP()/Boss.GetComponent<BossController>().MaxHP);
        }
    }
}

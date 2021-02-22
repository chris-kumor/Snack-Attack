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
        BossHealthBar.fillAmount = (float) (Boss.GetComponent<BossController>().HP)/100;
    }
}

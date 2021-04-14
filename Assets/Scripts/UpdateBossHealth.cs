using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBossHealth : MonoBehaviour
{
    public Image BossHealthBar;
    public GameObject Boss;
    private BossController bossController;
  

    // Start is called before the first frame update
    void Start()
    {
        bossController = Boss.GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss != null)
            BossHealthBar.fillAmount = (bossController.HP/bossController.MaxHP);
    }
}

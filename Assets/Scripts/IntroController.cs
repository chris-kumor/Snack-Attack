using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour{

    public GameObject boss;
    private BossController bossController;

    // Start is called before the first frame update
    void Start(){
        bossController = boss.GetComponent<BossController>();
        GameStats.isBattle = false;
    }

    // Update is called once per frame
    void Update(){
        if (Sinput.GetButton("Join", GameStats.MeleeSlot) || Sinput.GetButton("Join", GameStats.RangedSlot)){
            bossController.StartBattle();
            Destroy(gameObject);
        }
    }
}

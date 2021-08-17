using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : MonoBehaviour{
    // Start is called before the first frame update
    public GameObject MinionSpawn1, MinionSpawn2;
    public GameObject AppleMinion;
    private GameObject minion1, minion2;
    void Start(){
        GameStats.minions = 0;
    }

    void SpawnMinions(){
        minion1 = Instantiate(AppleMinion, MinionSpawn1.transform.position, Quaternion.identity);
        minion2 = Instantiate(AppleMinion, MinionSpawn2.transform.position, Quaternion.identity);
        minion1 = null;
        minion2 = null;
    }
    // Update is called once per frame
    void Update(){
        if(GameStats.minions == 0 && GameStats.isBattle)
        {
            SpawnMinions();
            GameStats.minions = 2;
        }
    }
}

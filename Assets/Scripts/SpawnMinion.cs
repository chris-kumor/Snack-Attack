using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MinionSpawn1, MinionSpawn2;
    public GameObject AppleMinion;
    public int maxMinion;

    private GameObject[] minions;

    private bool isMinnions;

    void Start()
    {
        isMinnions = false;
    }


    void anyMinions()
    {
        minions = GameObject.FindGameObjectsWithTag("minion");
        if(minions.Length != 0)
            isMinnions = true;
        else
            isMinnions = false; 

    }

    void SpawnMinions()
    {
        if(GameStats.isBattle)
        {
            Instantiate(AppleMinion, MinionSpawn1.transform.position, Quaternion.identity);
            Instantiate(AppleMinion, MinionSpawn2.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        anyMinions();
        if(!isMinnions)
            SpawnMinions();
    }
}

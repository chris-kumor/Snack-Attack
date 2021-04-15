using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MinionSpawn1, MinionSpawn2;
    public GameObject AppleMinion;

    public int minions;

    void Wait()
    {

    }

    void Start()
    {
        minions = 0;
    }

    void SpawnMinions()
    {
        Instantiate(AppleMinion, MinionSpawn1.transform.position, Quaternion.identity);
        Instantiate(AppleMinion, MinionSpawn2.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(minions == 0 && GameStats.isBattle)
        {
            SpawnMinions();
            minions += 2;
        }
    }
}

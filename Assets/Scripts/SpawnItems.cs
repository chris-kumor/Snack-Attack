using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject HealthPickUp, ShieldPickUp;
    public int maxItems;
    private GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        items = GameObject.FindGameObjectsWithTag("PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        items = GameObject.FindGameObjectsWithTag("PickUp");
        if(items.Length == 0 && GameStats.isBattle)
        {
            for(int i = 1; i <= (maxItems/2); i++)
            {
                Instantiate(HealthPickUp, new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
                Instantiate(ShieldPickUp, new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
            }
        }
    }
}

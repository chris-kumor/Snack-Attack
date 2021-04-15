using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject HealthPickUp, ShieldPickUp;
    public int maxItems;
    public int items;

    // Start is called before the first frame update
    void Start()
    {
        items = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(items == 0 && GameStats.isBattle)
        {

            Instantiate(HealthPickUp, new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
            Instantiate(HealthPickUp, new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
            Instantiate(ShieldPickUp, new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
            Instantiate(ShieldPickUp, new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-16.0f, 13.0f), 1.00f), Quaternion.identity);
            items = maxItems;
        }
    }
}

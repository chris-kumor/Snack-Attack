using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadAtkController : MonoBehaviour
{
    public float spread;
    public GameObject atkObj;

    // Start is called before the first frame update
    void Start()
    {
    
        GameObject atk = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
        atk.transform.right = transform.right;
        atk = null;
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + spread, transform.rotation.w);
        atk = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
        atk.transform.right = transform.right;
        atk = null;
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - (spread * 2), transform.rotation.w);
        atk = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
        atk.transform.right = transform.right;
        atk = null;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

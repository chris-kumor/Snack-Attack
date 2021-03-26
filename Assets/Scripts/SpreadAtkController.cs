using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadAtkController : MonoBehaviour
{
    public float spread;
    public GameObject atkObj;
    private Vector3 initAngle;
    private Vector3 initUp;

    // Start is called before the first frame update
    void Start()
    {

        initAngle = transform.right;
        initUp = transform.up;
        GameObject atk = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
        atk.transform.right = transform.right;
        atk = null;

        transform.right = Vector3.Slerp(initAngle, transform.up, spread);
        atk = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
        atk.transform.right = transform.right;
        atk = null;

        transform.right = Vector3.Slerp(initAngle, -initUp, spread);
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
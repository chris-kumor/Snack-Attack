using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadAtkController : MonoBehaviour
{
    public float spread;
    public GameObject atkObj;
    private Vector3 initAngle;
    private Vector3 initUp;
    
    private GameObject Boss;
    private GameObject[] atk = new GameObject[3];


    private int shellsAlive;

    public void removeShell()
    {
        shellsAlive-= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindWithTag("Boss");
        shellsAlive = 3;
        initAngle = transform.right;
        initUp = transform.up;
        for(int i = 0; i < atk.Length; i++)
        {
            if(i != 0)
                transform.right = Vector3.Slerp(initAngle, transform.up, spread);
            atk[i] = PlayerController.Attack(atkObj, transform.position, transform.right, 0, transform.rotation);
            atk[i].transform.right = transform.right;
            atk[i] = null;
        }
    }
    void Update()
    {
        if(shellsAlive == 0)
        {
            Boss.SendMessage("CanAttack");
            Destroy(gameObject);
        }
    }
}
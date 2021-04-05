using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{

    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Sinput.GetButton("Join", GameStats.MeleeSlot) || Sinput.GetButton("Join", GameStats.RangedSlot))
        {
            boss.SendMessage("StartBattle");
            Destroy(gameObject);
        }
    }
}

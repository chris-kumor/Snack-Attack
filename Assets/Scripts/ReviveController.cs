using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MeleePlayer, RangedPlayer;
    public Text ReviveStatus;

    // Update is called once per frame
    void Update()
    {
        if(!RangedPlayer.GetComponent<PlayerController>().isAlive)
            ReviveStatus.text = "Melee Player, revive Ranged Player.";
        else if(!MeleePlayer.GetComponent<PlayerController>().isAlive)
            ReviveStatus.text = "Ranged Player, revive Melee Player.";
        else
            ReviveStatus.text = "";
    }
}

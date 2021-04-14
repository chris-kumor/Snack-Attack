using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MeleePlayer, RangedPlayer;
    public Text ReviveStatus;
    private PlayerController rangedController, meleeController;

    void Start()
    {
        rangedController = RangedPlayer.GetComponent<PlayerController>();
        meleeController = MeleePlayer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rangedController.isAlive)
            ReviveStatus.text = "Melee Player, revive Ranged Player.";
        else if(!meleeController.isAlive)
            ReviveStatus.text = "Ranged Player, revive Melee Player.";
        else
            ReviveStatus.text = "";
    }
}

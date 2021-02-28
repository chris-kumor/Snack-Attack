using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMeleeHealth : MonoBehaviour
{
    private Image MeleePlayerHealthBar;
    private GameObject MeleePlayer;
    // Start is called before the first frame update
    void Start()
    {
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        MeleePlayerHealthBar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MeleePlayer != null)
        {
            MeleePlayerHealthBar.fillAmount = (MeleePlayer.GetComponent<PlayerController>().GetPlayerHP()/MeleePlayer.GetComponent<PlayerController>().MaxHP);
        }
    }
}

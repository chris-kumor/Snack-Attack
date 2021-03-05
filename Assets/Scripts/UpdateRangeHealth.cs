using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRangeHealth : MonoBehaviour
{
    private Image RangedPlayerHealthBar;
    public GameObject RangedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
        RangedPlayerHealthBar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RangedPlayer != null)
        {
            RangedPlayerHealthBar.fillAmount = (RangedPlayer.GetComponent<PlayerController>().GetPlayerHP()/RangedPlayer.GetComponent<PlayerController>().MaxHP);
        }
    }
}

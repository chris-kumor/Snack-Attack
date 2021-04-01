using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenuController : MonoBehaviour
{
    
    public Button PlayButton;
    public Text MeleeControls, RangedControls;
    public Text WarningText;

    
    void Start()
    {
        //will cause both to be any
        GameStats.MeleeSlot = Sinput.GetSlotPress("Join");
        GameStats.RangedSlot = Sinput.GetSlotPress("Join");

    }
    // Update is called once per frame
    void Update()
    {
        if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.any)
        {
            GameStats.MeleeSlot = Sinput.GetSlotPress("Join");
            if(GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any)
                MeleeControls.text = GameStats.MeleeSlot.ToString();
        }
        else if(GameStats.RangedSlot == SinputSystems.InputDeviceSlot.any)
        {
            GameStats.RangedSlot = Sinput.GetSlotPress("Join");
            if(GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any)
                RangedControls.text = GameStats.RangedSlot.ToString();

        }

        if(GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any && GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any && GameStats.MeleeSlot != GameStats.RangedSlot)
        {
            PlayButton.gameObject.SetActive(true);
        }
        else
        {
            PlayButton.gameObject.SetActive(false);
        }

    }

}

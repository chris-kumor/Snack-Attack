using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenuController : MonoBehaviour
{
    
    public Button PlayButton;
    public Image MeleeControls, RangedControls;
    public Sprite Controller, KBM, defultImage;

    

    
    void Start()
    {
        //will cause both to be any
        GameStats.MeleeSlot = Sinput.GetSlotPress("Join");
        GameStats.RangedSlot = Sinput.GetSlotPress("Join");
        GameStats.bothPlayersKB = false;
    

    }

    public void clearControlIcons()
    {
        MeleeControls.sprite = defultImage;
        RangedControls.sprite = defultImage;
        GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.any;
        GameStats.RangedSlot = SinputSystems.InputDeviceSlot.any;

    }
    // Update is called once per frame
    void Update()
    {
        //IF melee controls undecided
        if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.any)
        {
            GameStats.MeleeSlot = Sinput.GetSlotPress("Join");
            if(GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any)
            {
                if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
                    MeleeControls.sprite = KBM;
                else if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.gamepad1 || GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.gamepad2)
                    MeleeControls.sprite = Controller;
            }
               
        }
        //
        else if(GameStats.RangedSlot == SinputSystems.InputDeviceSlot.any)
        {
            GameStats.RangedSlot = Sinput.GetSlotPress("Join");
            if(GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any)
            {
                if(GameStats.RangedSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
                    RangedControls.sprite = KBM;
                else if(GameStats.RangedSlot == SinputSystems.InputDeviceSlot.gamepad1 || GameStats.RangedSlot == SinputSystems.InputDeviceSlot.gamepad2)
                    RangedControls.sprite = Controller;
            }

        }

        if(GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any && GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any)
        {
            PlayButton.gameObject.SetActive(true);
            if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse && GameStats.RangedSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse)
                GameStats.bothPlayersKB = true;
        }
       /*
        else if(GameStats.RangedSlot == GameStats.MeleeSlot)
        {
            PlayButton.gameObject.SetActive(false);
            GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.any;
            GameStats.RangedSlot = SinputSystems.InputDeviceSlot.any;
            MeleeControls.sprite = null;
            RangedControls.sprite = null;
        }
        */
        else
            PlayButton.gameObject.SetActive(false);

    }

}

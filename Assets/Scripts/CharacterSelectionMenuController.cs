using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenuController : MonoBehaviour
{
    
    public Button PlayButton;
    public Slider RangedInputSlider, RangedPlayerSlider, MeleeInputSlider, MeleePlayerSlider;
 

    

    
    void Start()
    {
        
        PlayButton.interactable = false;

    }

  
    // Update is called once per frame
    void Update()
    {
        if(!PlayButton.interactable)
        {
            if(RangedInputSlider.value == 0)
                GameStats.RangedSlot = SinputSystems.InputDeviceSlot.keyboardAndMouse;
            else if(RangedInputSlider.value == 1)
            {
                if(RangedPlayerSlider.value == 0)
                    GameStats.RangedSlot = SinputSystems.InputDeviceSlot.gamepad1;
                else if(RangedPlayerSlider.value == 1)
                    GameStats.RangedSlot = SinputSystems.InputDeviceSlot.gamepad2;
            }
            if(MeleeInputSlider.value == 0)
                GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.keyboardAndMouse;
            else if(MeleeInputSlider.value == 1)
            {
                if(MeleePlayerSlider.value == 0)
                    GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.gamepad1;
                else if(MeleePlayerSlider.value == 1)
                    GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.gamepad2;
            }
            if(RangedPlayerSlider.value != MeleePlayerSlider.value && (GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any ||GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any))
                PlayButton.interactable = true;
        }
        else if(RangedPlayerSlider.value == MeleePlayerSlider.value)
        {
            PlayButton.interactable = false;
        }

        GameStats.bothPlayersKB = (GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse && GameStats.RangedSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse);
            
    }

}

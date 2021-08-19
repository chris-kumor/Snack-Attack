using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenuController : MonoBehaviour{
    public Button PlayButton;
    private  SinputSystems.InputDeviceSlot[] controlTypes = new SinputSystems.InputDeviceSlot[2];
    private SinputSystems.InputDeviceSlot[] gamePads = new SinputSystems.InputDeviceSlot[2];
    public Slider RangedInputSlider, RangedPlayerSlider, MeleeInputSlider, MeleePlayerSlider;
    private bool bothController;
    void Start(){
        PlayButton.interactable = false;
        RangedPlayerSlider.interactable = false;
        MeleePlayerSlider.interactable = false;
        controlTypes[0] = SinputSystems.InputDeviceSlot.keyboardAndMouse;
        controlTypes[1] = SinputSystems.InputDeviceSlot.gamepad1;
        gamePads[0] = SinputSystems.InputDeviceSlot.gamepad1;
        gamePads[1] = SinputSystems.InputDeviceSlot.gamepad2;
        GameStats.RangedSlot = SinputSystems.InputDeviceSlot.any;
        GameStats.MeleeSlot = SinputSystems.InputDeviceSlot.any;
    }
    // Update is called once per frame
    void Update(){
        bothController = (int)RangedInputSlider.value == 1 && (int)MeleeInputSlider.value == 1;
        RangedPlayerSlider.interactable = bothController;
        MeleePlayerSlider.interactable = bothController;
        Debug.Log((int)RangedInputSlider.value);
        Debug.Log((int)MeleeInputSlider.value);
        if(!PlayButton.interactable){
            GameStats.RangedSlot = controlTypes[(int)RangedInputSlider.value];
            GameStats.MeleeSlot = controlTypes[(int)MeleeInputSlider.value];
            if(GameStats.RangedSlot == GameStats.MeleeSlot){
                GameStats.bothPlayersKB = (GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse && GameStats.RangedSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse);
                //Debug.Log(GameStats.bothPlayersKB);
                if(!GameStats.bothPlayersKB){
                    GameStats.RangedSlot = gamePads[(int)RangedPlayerSlider.value];
                    GameStats.MeleeSlot = gamePads[(int)MeleePlayerSlider.value];
                }
            }
            Debug.Log((GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any ||GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any) && (GameStats.RangedSlot != GameStats.MeleeSlot || GameStats.bothPlayersKB));
            if((GameStats.RangedSlot != SinputSystems.InputDeviceSlot.any ||GameStats.MeleeSlot != SinputSystems.InputDeviceSlot.any) && (GameStats.RangedSlot != GameStats.MeleeSlot || GameStats.bothPlayersKB))
                PlayButton.interactable = true;
        }
        else if(RangedPlayerSlider.value == MeleePlayerSlider.value && (RangedInputSlider.value == 1 && MeleeInputSlider.value == 1) ){
            PlayButton.interactable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        if(GameStats.MeleeSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse || GameStats.RangedSlot == SinputSystems.InputDeviceSlot.keyboardAndMouse){
            Cursor.visible = true;
        }
        GameStats.ShieldTimer = 10.00f;
    }
}

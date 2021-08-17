using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        DisableMenu();
    }

    public void EnableMenu(){
        gameObject.SetActive(true);
    }

    public void DisableMenu(){
        gameObject.SetActive(false);
    }
}

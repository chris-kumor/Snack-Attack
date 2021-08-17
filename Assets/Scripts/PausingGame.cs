using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PausingGame : MonoBehaviour{
    
    public Button[] UIButtons;
    public Text PausedText;
    // Start is called before the first frame update
    void Start(){
        GameStats.isPaused = false;
        //PausedText = GameObject.FindWithTag("PauseText").GetComponent<Text>();
        PausedText.enabled = false;
        toggleButtons(false);
    }
    void toggleButtons(bool status){   
        foreach(Button button in UIButtons)
            button.gameObject.SetActive(status);
    }
    public void buttonUIResume(){
        if(GameStats.isPaused && GameStats.isBattle){
            GameStats.isPaused = false;
            Time.timeScale = 1;
            PausedText.enabled = false;
            toggleButtons(false);
        }
    }
    // Update is called once per frame
    void Update(){
        if(Sinput.GetButtonDown("Pause") && !GameStats.isPaused && GameStats.isBattle){
             GameStats.isPaused = true;
            Time.timeScale = 0;
            PausedText.enabled = true;
            toggleButtons(true);
        }
        else if(Sinput.GetButtonDown("Pause") && GameStats.isPaused && GameStats.isBattle){
             GameStats.isPaused = false;
            Time.timeScale = 1;
            PausedText.enabled = false;
            toggleButtons(false);
        }
    }
}

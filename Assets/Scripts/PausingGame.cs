using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausingGame : MonoBehaviour
{
    public bool isPaused;
    public Text PausedText;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        //PausedText = GameObject.FindWithTag("PauseText").GetComponent<Text>();
        PausedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Sinput.GetButtonDown("Pause") && !isPaused && GameStats.isBattle == true)
        {
            isPaused = true;
            Time.timeScale = 0;
            PausedText.enabled = true;
        }
        else if(Sinput.GetButtonDown("Pause") && isPaused && GameStats.isBattle == true)
        {
            isPaused = false;
            Time.timeScale = 1;
            PausedText.enabled = false;
        }

    }
}

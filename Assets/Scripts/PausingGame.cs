using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausingGame : MonoBehaviour
{
    public string[] pauseButtons = new string[2];
    public bool isPaused;
    
    private Text PausedText;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        PausedText = GameObject.FindWithTag("PauseText").GetComponent<Text>();
        PausedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pauseButtons.Length; i++)
        {
            if(Input.GetKeyDown(pauseButtons[i]) && !isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                PausedText.enabled = true;
            }
            else if(Input.GetKeyDown(pauseButtons[i]) && isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
                PausedText.enabled = false;
            }
        }
    }
}

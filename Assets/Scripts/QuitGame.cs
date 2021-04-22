using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void QuitingGame()
    {
        Invoke("Quit", 1.0f);
    }
    // Start is called before the first frame update

}


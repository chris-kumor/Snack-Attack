using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPlayerSelection : MonoBehaviour
{
    void Play()
    {
         SceneManager.LoadScene("Character Selection", LoadSceneMode.Single);
    }
    public void LoadingMainScene()
    {
        Invoke("Play", 3.0f);
    }


}

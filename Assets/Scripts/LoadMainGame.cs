using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGame : MonoBehaviour
{
    void Play()
    {
         SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    public void LoadingMainScene()
    {
        Invoke("Play", 3.0f);
    }



}


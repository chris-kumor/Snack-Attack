using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void PlayGame()
    {
         Invoke("LoadingMainScene", 3.0f);
    }
    public void LoadingMainScene()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    public void LoadingLocalCharacterSelection()
    {
         SceneManager.LoadScene("LocalMenu", LoadSceneMode.Single);
    } 
    public void LocalCharacterSelection()
    {
        Invoke("LoadingLocalCharacterSelection", 3.0f);
    }
    public void loadingOnlineMenu()
    {
        SceneManager.LoadScene("OnlineMenu", LoadSceneMode.Single);
    }
    public void OnlineMenu()
    {
        Invoke("loadingOnlineMenu", 3.0f);
    }

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

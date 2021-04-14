using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningCondition : MonoBehaviour
{
    public GameObject Boss, MeleePlayer, RangedPlayer;
    private PlayerController meleeController, rangedController;

    public float sceneDelayTimer;

    void WaitToChangeScene()
    {
        SceneManager.LoadScene("Results", LoadSceneMode.Single);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        GameStats.isBattle = false;
        meleeController = MeleePlayer.GetComponent<PlayerController>();
        rangedController = RangedPlayer.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null || (!(rangedController.isAlive) && !(meleeController.isAlive)))
        {
            Debug.Log("The game is over!");
            if(Boss == null)
                GameStats.isBossAlive = false;
            else
                GameStats.isBossAlive = true;

            Invoke("WaitToChangeScene", sceneDelayTimer);
            
        }
    }
}

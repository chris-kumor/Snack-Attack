using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningCondition : MonoBehaviour
{
    GameObject MeleePlayer, RangedPlayer;
    public GameObject Boss;
    public AudioSource gameAudioSource;

    public AudioClip BossDying;
    private PlayerController meleeController, rangedController;

    public float sceneDelayTimer;

    void WaitToChangeScene()
    {
        SceneManager.LoadScene("Results", LoadSceneMode.Single);
    }

    void findMelee()
    {
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        meleeController = MeleePlayer.GetComponent<PlayerController>();
    }

    void findRanged()
    {
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        rangedController = RangedPlayer.GetComponent<PlayerController>();
    }
    void findPlayers()
    {
        findMelee();
        findRanged();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        Boss =  GameObject.FindWithTag("Boss");
        findPlayers();
        GameStats.isBattle = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(MeleePlayer == null)
            findMelee();
        if(RangedPlayer == null)
            findRanged();
        if(Boss == null || (!(rangedController.isAlive) && !(meleeController.isAlive)))
        {
            if(Boss == null)
            {
                GameStats.isBossAlive = false;
                gameAudioSource.PlayOneShot(BossDying, 0.5f);
            }
            else
                GameStats.isBossAlive = true;
            Invoke("WaitToChangeScene", sceneDelayTimer);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningCondition : MonoBehaviour
{
    private GameObject Boss;
    private GameObject MeleePlayer;
    private GameObject RangedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindWithTag("Boss");
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null || (MeleePlayer == null && RangedPlayer == null))
        {
            if(Boss == null)
            {
                GameStats.isBossAlive = false;
            }
            else
                GameStats.isBossAlive = true;
            SceneManager.LoadScene("Results", LoadSceneMode.Single);
        }
    }
}

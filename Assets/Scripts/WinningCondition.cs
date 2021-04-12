using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningCondition : MonoBehaviour
{
    public GameObject Boss, MeleePlayer, RangedPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        GameStats.isBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null || (MeleePlayer == null && RangedPlayer == null))
        {
            if(Boss == null)
                GameStats.isBossAlive = false;
            else
                GameStats.isBossAlive = true;
            SceneManager.LoadScene("Results", LoadSceneMode.Single);
        }
    }
}

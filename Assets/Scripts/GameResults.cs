using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResults : MonoBehaviour
{
   
    public Text GameResult;
    int MeleePlayerDamage, RangedPlayerDamage;
    public Text PlayersDamageText;
    // Start is called before the first frame update
    public void Awake()
    {

        MeleePlayerDamage = GameStats.MeleeDamage;
        RangedPlayerDamage = GameStats.RangedDamage;
        
    }
    // Update is called once per frameS
    public void Start()
    {
        Cursor.visible = true;
        //Debug.Log("Bandits had " + BanditsGold);
        //Debug.Log("Guards had " + GuardsGold);
        if(!(GameStats.isBossAlive))
        {
            GameResult.text = "The players have emerged victorious!";
        }
        else if(GameStats.isBossAlive)
        {
            GameResult.text = "The players were defeated.";
        }

        PlayersDamageText.text = "Melee Player Damage: " + GameStats.MeleeDamage + "/n" + "Ranged Player Damage: " + GameStats.RangedDamage;

    }
    public void Update()
    {
        Cursor.visible = true;
    }
}

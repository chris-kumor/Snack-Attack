using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResults : MonoBehaviour
{
   
    public Text GameResult;
    public Text PlayersDamageText;
    
    private float MeleePlayerDamage, RangedPlayerDamage;
    
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
        if(GameStats.isBossAlive)
        {
            GameResult.text = "The players were defeated.";
        }
        else
        {
            GameResult.text = "The players have emerged victorious!";
        }

        PlayersDamageText.text = "Melee Player Damage: " + GameStats.MeleeDamage + "    Ranged Player Damage: " + GameStats.RangedDamage;

    }
    public void Update()
    {
        Cursor.visible = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenuController : MonoBehaviour
{
    
    public Scrollbar RangedControlScrollBar, RangedPlayerScrollBar, MeleeControlScrollBar, MeleePlayerScrollBar;
    public Button PlayButton;
    public Text WarningText;

    // Update is called once per frame
    void Update()
    {

        GameStats.MeleeCharacter = Mathf.RoundToInt(MeleePlayerScrollBar.value);
        GameStats.MeleeControls = Mathf.RoundToInt(MeleeControlScrollBar.value);
        GameStats.RangedControls = Mathf.RoundToInt(RangedControlScrollBar.value);
        GameStats.RangedCharacter = Mathf.RoundToInt(RangedPlayerScrollBar.value);
        //Debug.Log("GameStats.MeleeCharacter" + GameStats.MeleeCharacter);
        //Debug.Log("GameStats.RangeCharacter" + GameStats.RangedCharacter);

        if(GameStats.MeleeCharacter == GameStats.RangedCharacter)
        {
            Debug.Log("Prevents the game starting.");
            PlayButton.gameObject.SetActive(false);
            WarningText.gameObject.SetActive(true);
        }
        else
        {
            PlayButton.gameObject.SetActive(true);
            WarningText.gameObject.SetActive(false);
        }
    }
}

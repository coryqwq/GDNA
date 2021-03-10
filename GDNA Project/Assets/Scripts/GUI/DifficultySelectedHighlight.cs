using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectedHighlight : MonoBehaviour
{
    public GameObject[] buttons;
    public int indexSelected;
 
    public void SetIndexEasy()
    {
        PlayerPrefs.SetInt("DifficultyIndex", 0);
    }
    public void SetIndexNormal()
    {
        PlayerPrefs.SetInt("DifficultyIndex", 1);
    }
    public void SetIndexHard()
    {
        PlayerPrefs.SetInt("DifficultyIndex", 2);
    }
    public void SetIndexVeryHard()
    {
        PlayerPrefs.SetInt("DifficultyIndex", 3);
    }

    public void HighlightButton()
    {
        for (int index = 0; index < buttons.Length; index++)
        {
            if (index != PlayerPrefs.GetInt("DifficultyIndex"))
            {
                buttons[index].GetComponent<ButtonFX>().DifficultyDeselected();
            }
        }

        buttons[PlayerPrefs.GetInt("DifficultyIndex")].GetComponent<ButtonFX>().DifficultySelected();
    }
}

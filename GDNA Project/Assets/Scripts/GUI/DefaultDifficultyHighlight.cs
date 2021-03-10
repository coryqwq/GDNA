using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDifficultyHighlight : MonoBehaviour
{
    public GameObject[] buttons;

    void Start()
    {
        //highlight the default difficulty button
        buttons[PlayerPrefs.GetInt("DifficultyIndex")].GetComponent<ButtonFX>().DifficultySelected();
    }
}

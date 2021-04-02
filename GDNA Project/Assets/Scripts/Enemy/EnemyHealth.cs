using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject gameState;
    GameState gameStateScript;

    public float currentHP = 750;
    public float maxHP = 750;
    public int points = 6900;

    public RectTransform healthBar;
    public float healthBarMax = 1100;

    void Start()
    {
        gameStateScript = gameState.GetComponent<GameState>();
        //currentHP *= gameStateScript.enemyHealthMultiplier;

        maxHP = currentHP;
        healthBarMax = healthBar.sizeDelta.x;

    }
}

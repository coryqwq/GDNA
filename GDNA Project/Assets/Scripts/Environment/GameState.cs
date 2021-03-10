using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public float spawnIntervalOffset = 1;
    public float projectileSpeedOffset = 0;
    public float scoreMultiplier = 1;

    public float easyInterval = 2.0f;
    public float easySpeed = -3.0f;
    public float easyScoreMultiplier = 0.5f;

    public float mediumInterval = 1.0f;
    public float mediumSpeed = 0.0f;
    public float mediumScoreMultiplier = 1.0f;

    public float hardInterval = 0.5f;
    public float hardSpeed = 3.0f;
    public float hardScoreMultiplier = 2.0f;

    public float hardestInterval = 0.33f;
    public float hardestSpeed = 6.0f;
    public float hardestScoreMultiplier = 3.0f;

    public void Start()
    {
        //initialize difficulty index for first run of application
        if (PlayerPrefs.GetInt("FirstRun", 0) == 0)
        {
            PlayerPrefs.SetInt("DifficultyIndex", 1);
            PlayerPrefs.SetInt("FirstRun", 1);
        }

        //initiliaze spawn interval and score multiplier for level
        spawnIntervalOffset = PlayerPrefs.GetFloat("ModeSpawnInterval", 0);
        scoreMultiplier = PlayerPrefs.GetFloat("ModeScoreMultiplier", 0);
    }
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", easyInterval);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", easyScoreMultiplier);
    }
    public void SetMediumDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", mediumInterval);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", mediumScoreMultiplier);
    }
    public void SetHardDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", hardInterval);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", hardScoreMultiplier);
    }

    public void SetHardestDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", hardestInterval);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", hardestScoreMultiplier);
    }
}

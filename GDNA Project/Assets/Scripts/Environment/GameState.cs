using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public float spawnIntervalOffset = 1;
    public float projectileSpeedOffset = 1;
    public float scoreMultiplier = 1;
    public float enemyHealthMultiplier = 1;

    public float easyInterval = 2.0f;
    public float easySpeed = 0.75f;
    public float easyScoreMultiplier = 0.5f;
    public float easyEnemyHealthMultiplier = 0.75f;

    public float mediumInterval = 1.0f;
    public float mediumSpeed = 1.0f;
    public float mediumScoreMultiplier = 1.0f;
    public float mediumEnemyHealthMultiplier = 1.0f;

    public float hardInterval = 0.5f;
    public float hardSpeed = 1.5f;
    public float hardScoreMultiplier = 2.0f;
    public float hardEnemyHealthMultiplier = 1.1f;

    public float hardestInterval = 0.33f;
    public float hardestSpeed = 2.0f;
    public float hardestScoreMultiplier = 3.0f;
    public float hardestEnemyHealthMultiplier = 1.2f;

    public bool levelDialogue = true;
    public bool levelPause = false;
    public bool levelEnd = false;
    public bool levelComplete = false;
    public bool levelStart = true;

    public Animator startLevelAnim;
    public Animator endLevelAnim;
    public GameObject mainCamera;
    public GameObject dialogueTrigger;
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", easyInterval);
        PlayerPrefs.SetFloat("ModeProjectileSpeed", easySpeed);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", easyScoreMultiplier);
        PlayerPrefs.SetFloat("ModeEnemyHealthMultiplier", easyEnemyHealthMultiplier);
    }
    public void SetMediumDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", mediumInterval);
        PlayerPrefs.SetFloat("ModeProjectileSpeed", mediumSpeed);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", mediumScoreMultiplier);
        PlayerPrefs.SetFloat("ModeEnemyHealthMultiplier", mediumEnemyHealthMultiplier);
    }
    public void SetHardDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", hardInterval);
        PlayerPrefs.SetFloat("ModeProjectileSpeed", hardSpeed);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", hardScoreMultiplier);
        PlayerPrefs.SetFloat("ModeEnemyHealthMultiplier", hardEnemyHealthMultiplier);
    }

    public void SetHardestDifficulty()
    {
        PlayerPrefs.SetFloat("ModeSpawnInterval", hardestInterval);
        PlayerPrefs.SetFloat("ModeProjectileSpeed", hardestSpeed);
        PlayerPrefs.SetFloat("ModeScoreMultiplier", hardestScoreMultiplier);
        PlayerPrefs.SetFloat("ModeEnemyHealthMultiplier", hardestEnemyHealthMultiplier);
    }

    private void Start()
    {
        //initialize difficulty index for first run of application
        if (PlayerPrefs.GetInt("FirstRun", 0) == 0)
        {
            PlayerPrefs.SetInt("DifficultyIndex", 1);
            PlayerPrefs.SetInt("FirstRun", 1);
        }


        //initiliaze spawn interval and score multiplier for level
        spawnIntervalOffset = PlayerPrefs.GetFloat("ModeSpawnInterval", 1);
        projectileSpeedOffset = PlayerPrefs.GetFloat("ModeProjectileSpeed", 1);
        scoreMultiplier = PlayerPrefs.GetFloat("ModeScoreMultiplier", 1);
        enemyHealthMultiplier = PlayerPrefs.GetFloat("ModeEnemyHealthMultiplier", 1);

    }

    private void Update()
    {
        if(levelDialogue == false && levelStart == true)
        {
            startLevelAnim.SetBool("FadeIn", true);
            GetComponent<AudioSource>().Play();
            levelStart = false;
        }

        if (startLevelAnim.GetCurrentAnimatorStateInfo(0).IsName("WarningFadeOut"))
        {
            startLevelAnim.SetBool("FadeIn", false);
        }

        if (levelComplete == true)
        {
            StartCoroutine(DelayEndLevel());
        }

    }

    IEnumerator DelayEndLevel()
    {
        yield return new WaitForSeconds(4f);
        if(levelEnd == false)
        {
            endLevelAnim.SetTrigger("FadeIn");
            FadeOutBGM fadeOutBGMScript = mainCamera.GetComponent<FadeOutBGM>();
            fadeOutBGMScript.FadeOut();
            yield return new WaitForSeconds(6f);
            dialogueTrigger.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyStatus : MonoBehaviour
{
    public bool isAlive = true;

    EnemyController enemyControllerScript;
    EnemyHealth enemyHealthScript;
    SpriteRenderer enemyColor;
    Pod042Controller pod042ControllerScript;
    Pod153Controller pod153ControllerScript;
    GameState gameStateScript;
    Score scoreScript;

    public GameObject enemy;
    public GameObject pod042;
    public GameObject pod153;
    public GameObject gameState;
    public GameObject score;
    public GameObject lastBlast;
    public GameObject blast;

    public float playerSwordDamage = 200f;

    public bool takingDamage = false;
    public float elasped = 0;

    public GameObject damagePopupText;
    TextMeshPro textMesh;

    private void Start()
    {

        enemyHealthScript = enemy.GetComponent<EnemyHealth>();
        enemyColor = enemy.GetComponent<SpriteRenderer>();
        gameStateScript = gameState.GetComponent<GameState>();
        scoreScript = score.GetComponent<Score>();
        enemyControllerScript = enemy.GetComponent<EnemyController>();
        pod042ControllerScript = pod042.GetComponent<Pod042Controller>();
        pod153ControllerScript = pod153.GetComponent<Pod153Controller>();
        textMesh = damagePopupText.GetComponent<TextMeshPro>();

    }

    private void Update()
    {
        playerSwordDamage = Random.Range(400, 420);

        elasped += Time.deltaTime;

        if (takingDamage)
        {
            elasped = 0;
            enemyColor.color = new Color(255, 0, 0);
        }

        if (!takingDamage && elasped > 0.1f)
        {
            enemyColor.color = new Color(255, 255, 255);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectileBullet"))
        {
            enemyHealthScript.currentHP -= pod042ControllerScript.projectileDamage;
            DisplayDamagePopup((int)pod042ControllerScript.projectileDamage);
            enemyHealthScript.healthBar.sizeDelta = new Vector2((enemyHealthScript.currentHP * (enemyHealthScript.healthBarMax / enemyHealthScript.maxHP))
                                                                , enemyHealthScript.healthBar.sizeDelta.y);

            scoreScript.temp = (int)(pod042ControllerScript.projectileDamage * gameStateScript.scoreMultiplier);
            scoreScript.scoreNumber += (int)pod042ControllerScript.projectileDamage;

            takingDamage = true;
            StartCoroutine(DelayRecolor());

        }

        if (other.CompareTag("PlayerProjectileLaser"))
        {
            enemyHealthScript.currentHP -= pod153ControllerScript.projectileDamage;
            DisplayDamagePopup((int)pod153ControllerScript.projectileDamage);
            enemyHealthScript.healthBar.sizeDelta = new Vector2((enemyHealthScript.currentHP * (enemyHealthScript.healthBarMax / enemyHealthScript.maxHP))
                                                                , enemyHealthScript.healthBar.sizeDelta.y);


            scoreScript.temp = (int)(pod153ControllerScript.projectileDamage * gameStateScript.scoreMultiplier);
            scoreScript.scoreNumber += (int)(pod153ControllerScript.projectileDamage * gameStateScript.scoreMultiplier);

            takingDamage = true;
            StartCoroutine(DelayRecolor());

        }

        if (other.CompareTag("PlayerSword"))
        {
            enemyHealthScript.currentHP -= playerSwordDamage;
            DisplayDamagePopup((int)playerSwordDamage);
            enemyHealthScript.healthBar.sizeDelta = new Vector2((enemyHealthScript.currentHP * (enemyHealthScript.healthBarMax / enemyHealthScript.maxHP))
                                                                , enemyHealthScript.healthBar.sizeDelta.y);
            
            scoreScript.temp = (int)(playerSwordDamage * gameStateScript.scoreMultiplier);
            scoreScript.scoreNumber += (int)(playerSwordDamage * gameStateScript.scoreMultiplier);

            takingDamage = true;
            StartCoroutine(DelayRecolor());

        }

        if (enemyHealthScript.currentHP <= 0)
        {
            if (isAlive == true)
            {
                isAlive = false;

                GetComponent<AudioSource>().Play();

                enemyControllerScript.x = 4;
                enemyControllerScript.y = 3;
                enemyControllerScript.attackInterval = 0.2f;

                InvokeRepeating("SelfDestruct", 0, 0.2f);
                Instantiate(blast, transform.position, transform.rotation);

                int index = 0;
                foreach (Transform child in this.transform)
                {
                    transform.GetChild(index).GetComponent<Collider>().enabled = false;
                    index++;
                }

                scoreScript.temp = (int)(enemyHealthScript.points * gameStateScript.scoreMultiplier);
                scoreScript.scoreNumber += (int)(enemyHealthScript.points * gameStateScript.scoreMultiplier);

                gameStateScript.levelComplete = true;

            }
        }
    }
    public void DisplayDamagePopup(int damageAmount)
    {
        damagePopupText.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
        Instantiate(damagePopupText, transform.position + new Vector3(Random.Range(-0.3f, 1f), Random.Range(1.0f, 2.5f), -1), transform.rotation);
    }

    void SelfDestruct()
    {
        Instantiate(lastBlast, transform.position + new Vector3(Random.Range(-1.8f, 1f), Random.Range(0.7f, 1.5f), 0), transform.rotation);
    }

    IEnumerator DelayRecolor()
    {
        yield return new WaitForSeconds(0.01f);
        takingDamage = false;
    }
}

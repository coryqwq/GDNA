using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator enemyAnim;
    EnemyFire enemyFireScript;
    EnemyStatus enemyStatusScript;
    public GameObject enemyCollider;
    public GameObject gameState;
    GameState gameStateScript;
    EnemyHealth enemyHealthScript;
    public AudioClip[] clip;
    public AudioSource[] audioSource;

    public float attackInterval = 1f;

    public Vector3 direction;
    public int x = 8;
    public int y = 3;

    public bool flag = false;
    public bool flag1 = false;
    public float elapsed = 0;

    public int healthThreshold;
    // Update is called once per frame

    void Start()
    {
        enemyFireScript = GetComponent<EnemyFire>();
        enemyStatusScript = enemyCollider.GetComponent<EnemyStatus>();
        enemyHealthScript = gameObject.GetComponent<EnemyHealth>();
        gameStateScript = gameState.GetComponent<GameState>();

        healthThreshold = (int)(enemyHealthScript.maxHP * 0.75f);

        enemyAnim.SetBool("EnterMecha", true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Quad1"))
        {
            direction = new Vector3(Random.Range(-x, 0), Random.Range(-y, 0), 0);
        }
        if (other.CompareTag("Quad2"))
        {
            direction = new Vector3(Random.Range(1, x), Random.Range(-y, 0), 0);
        }
        if (other.CompareTag("Quad3"))
        {
            direction = new Vector3(Random.Range(1, x), Random.Range(1, y), 0);
        }
        if (other.CompareTag("Quad4"))
        {
            direction = new Vector3(Random.Range(-x, 0), Random.Range(1, y), 0);
        }
    }

    void Update()
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyEnterMecha") && flag1 == false)
        {
            audioSource[0].PlayOneShot(clip[0]);
            flag1 = true;
        }

        if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Passive") && gameStateScript.levelComplete == false && gameStateScript.levelEnd == false)
        {
            if(flag == false && enemyStatusScript.isAlive == true)
            {
                enemyFireScript.StartEnemyPhase(Random.Range(0, 3));

                if(rb.velocity.x == 0 && enemyHealthScript.currentHP < healthThreshold)
                {
                    enemyFireScript.StartEnemyPhase(3);
                }

                flag = true;
            }

            elapsed += Time.deltaTime;

            if(elapsed > attackInterval * gameStateScript.spawnIntervalOffset)
            {
                rb.AddForce(direction);
                audioSource[0].PlayOneShot(clip[1]);

                elapsed = 0;
                flag = false;
            }
        }
    }
}


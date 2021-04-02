using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    //declaring and initializing variable
    public float HP = 10;
    public Animator playerAnim;
    public CameraShake cameraShakeScript;
    public GameObject playerCompound;
    public GameObject blast;
    public GameObject lastBlast;
    public Image [] health;
    public int indexHP = 0;
    public GameObject gameState;
    GameState gameStateScript;

    public GameObject sceneTransition;
    SceneTransition sceneTransitionScript;

    private void OnTriggerEnter(Collider other)
    {
        //check if on trigger enter with enemy projectile
        if (other.CompareTag("EnemyProjectile") && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerSelfDestruct"))
        {
            if(HP > 0)
            {
                playerAnim.SetBool("Damaged", true);
                StartCoroutine(cameraShakeScript.Shake(0.2f, 0.5f));
                HP -= 1;
                Destroy(health[indexHP].gameObject);
                Instantiate(blast, transform.position, transform.rotation);
                indexHP++;
            }
           
            //if player health equals 0, destroy player gameobject
            if (HP <= 0)
            {
                gameStateScript = gameState.GetComponent<GameState>();
                gameStateScript.levelEnd = true;

                sceneTransitionScript = sceneTransition.GetComponent<SceneTransition>();
                sceneTransitionScript.RetryMenu();

                Instantiate(lastBlast, transform.position, transform.rotation);
                Destroy(playerCompound);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text highScore;
    public Text score;
    public Text time;

    public float timer = 0;
    public int highScoreNumber = 0;
    public int scoreNumber = 0;
    public int scoreCount = 0;
    public int temp = 0;
    public int count = 100;

    PlayerStatus playerStatusScript;
    GameState gameStateScript;

    public GameObject player;
    public GameObject gameState;

    // Start is called before the first frame update
    void Start()
    {
        playerStatusScript = player.GetComponent<PlayerStatus>();
        gameStateScript = gameState.GetComponent<GameState>();

        //get the key value and display the high score
        highScore.text = "High Score:" + PlayerPrefs.GetInt("highscore", 0);
        //get the key value and assign to variable for high score
        highScoreNumber = PlayerPrefs.GetInt("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStateScript.levelDialogue == false && gameStateScript.levelPause == false && gameStateScript.levelEnd == false && gameStateScript.levelComplete == false)
        {
            timer += Time.deltaTime;
            time.text = "Time:" + timer;
        }

        
        if(temp > count && scoreCount < scoreNumber - count*2)
        {
            score.text = "Score:" + scoreCount;
            scoreCount += count;
        }
        
        if (scoreCount <= scoreNumber)
        {
            score.text = "Score:" + scoreCount;
            scoreCount++;

        }

        //update the high score and display it
        if (scoreNumber > highScoreNumber)
        {
            highScoreNumber = scoreNumber;
            highScore.text = "High Score:" + highScoreNumber;
        }

        //if player passses level, set the key value, and save it
        if (gameStateScript.levelEnd == true)
        {
            PlayerPrefs.SetInt("highscore", highScoreNumber);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; set; }


    private int score;
    private int highScore;
    private int currentLife;

    public Text scoreText;
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentLife = 5;
        score = 0;

        highScore = PlayerPrefs.GetInt("HighScore");
        highscoreText.text = "Highscore: " + "\n" + highScore;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
        Debug.Log("highscore = " +"\n"+ highScore);
        scoreText.text = "Score: "+ "\n" + score;

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
            highscoreText.text = "Highscore: " + "\n" + highScore;
        }
    }

}

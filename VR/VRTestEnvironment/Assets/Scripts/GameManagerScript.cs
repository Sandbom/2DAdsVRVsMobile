using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; set; }


    private int score;
    private int highScore;
    private float time;

    public Text scoreText;
    public Text highscoreText;
    public Text timeText;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        score = 0;
        time = 30f;

        highScore = PlayerPrefs.GetInt("HighScore");
        highscoreText.text = "Highscore: " + "\n" + highScore;

        StartCoroutine(Countdown());

    }

    // Update is called once per frame
    void Update()
    {/*
        time -= Time.deltaTime;
        if(time <= 0)
        {
            GameOver();
        }*/
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

    public IEnumerator Countdown(float timeValue = 30)
    {
        time = timeValue;
        while(time > 0)
        {
            timeText.text = "Time left: " + "\n" + "\n" + time;
            yield return new WaitForSeconds(1.0f);
            time--;
        }
        if(time <= 0)
        {
            GameOver();
        }
    }

    

    private void GameOver()
    {
        Debug.Log("Player has lost all Lives");
    }

}

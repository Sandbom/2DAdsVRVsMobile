using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Adverty;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; set; }

    //Score variables
    private int score;
    private int highScore;
    private int pointMultiplier;
    private int currentStreak;

    //Game control variables
    private float time;

    public Text scoreText;
    public Text highscoreText;
    public Text timeText;
    
    public GameObject twoMultiplierText;
    public GameObject threeMultiplierText;
    public GameObject fourMultiplierText;
    public GameObject sixMultiplierText;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        AdvertySDK.Init();
        score = 0;
        time = 30f;
        pointMultiplier = 1;
        currentStreak = 0;

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
        currentStreak++;
        // Fix actual streak value later and where they become relevant
        if (currentStreak == 2)
        {
            twoMultiplierText.SetActive(true);
            pointMultiplier = 2;
            Debug.Log("2x multiplier active");
        }
        else if (currentStreak == 5)
        {
            twoMultiplierText.SetActive(false);
            threeMultiplierText.SetActive(true);
            pointMultiplier = 3;
        }
        else if (currentStreak == 10)
        {
            threeMultiplierText.SetActive(false);
            fourMultiplierText.SetActive(true);
            pointMultiplier = 4;
        }
        else if (currentStreak == 15)
        {
            fourMultiplierText.SetActive(false);
            sixMultiplierText.SetActive(true);
            pointMultiplier = 6;
        }

        score += pointMultiplier;
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

    public void BreakStreak()
    {
        currentStreak = 0;
        pointMultiplier = 1;

        twoMultiplierText.SetActive(false);
        threeMultiplierText.SetActive(false);
        fourMultiplierText.SetActive(false);
        sixMultiplierText.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("Times up!");
    }

}

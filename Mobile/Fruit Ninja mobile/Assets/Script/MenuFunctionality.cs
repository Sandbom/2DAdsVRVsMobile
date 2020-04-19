using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Adverty;

public class MenuFunctionality : MonoBehaviour
{
    public Text titleText;
    public Text highScoreText;


    private int highScore;
    private int startSceneIndex;

    public void StartGame()
    {
        startSceneIndex = PlayerPrefs.GetInt("startScene");
        Debug.Log("Starting Scene: " + startSceneIndex);

        SceneManager.LoadScene(startSceneIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("startScene") == 2)
        {
            titleText.text = "Blackbeard's" + "\n" + "Tavern";
        }
        else
        {
            titleText.text = "Redbeard's" + "\n" + "Tavern";
        }

        highScore = PlayerPrefs.GetInt("Score");
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Adverty;

public class MenuFunctionality : MonoBehaviour
{

    public Text highScoreText;
    private int highScore;

    public void StartGame()
    {
        SceneManager.LoadScene(2);
        AdvertySDK.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScreenCapture.CaptureScreenshot("Screenshot-start");
        highScore = PlayerPrefs.GetInt("Score");
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

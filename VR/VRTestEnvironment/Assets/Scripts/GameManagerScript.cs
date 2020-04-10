using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; set; }


    private int score;
    private int highScore;
    private int currentLife;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentLife = 5;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

}

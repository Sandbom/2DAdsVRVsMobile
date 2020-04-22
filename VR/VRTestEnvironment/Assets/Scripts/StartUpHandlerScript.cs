using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartUpHandlerScript : MonoBehaviour
{

    public static StartUpHandlerScript Instance { set; get; }

    //public bool adfirst;

    private int startSceneIndex;
    private int scenesPlayed;
    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        Instance = this;
        DontDestroyOnLoad(gameObject);
        scenesPlayed = 0;
        
        if(PlayerPrefs.GetInt("startScene") == 0)
        {
            startSceneIndex = Random.Range(2, 4); //randoms between 2 and 3 cause of confusing syntax
            PlayerPrefs.SetInt("startScene", startSceneIndex);
            Debug.Log("Saving startSceneIndex: " + startSceneIndex);

        }

        SceneManager.LoadScene(1);
    }

    public void addScenePlayed()
    {
        scenesPlayed++;
    }

    public bool isFirstScenePlayed()
    {
        if (scenesPlayed == 1)
        {
            return true;
        }
        else return false;

    }
    public int getStartSceneIndex()
    {
        return PlayerPrefs.GetInt("startScene");
    }

}

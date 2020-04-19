using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Adverty;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { set; get; }

    public AudioClip[] allSounds;
    public AudioSource audioSource;

    public bool adsFirst = false;

    private int startSceneIndex;
    private int scenesPlayed;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Instance = this;
        DontDestroyOnLoad(gameObject);
        scenesPlayed = 0;

        if (PlayerPrefs.GetInt("startScene") == 0) // No startscene has been set so random a new one
        {
            startSceneIndex = Random.Range(2, 4); // Random between 2 and 3 (corresponds to scene indexes) , cant random with 0 included as that is playerprefs defaultValue
            PlayerPrefs.SetInt("startScene", startSceneIndex);
            Debug.Log("Saving startsceneIndex: " + startSceneIndex);
        }

        SceneManager.LoadScene(1);
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(allSounds[soundIndex], new Vector3(0,0,-50));
    }

    public void addScenePlayed()
    {
        scenesPlayed++;
    }

    public bool isFirstScenePlayed()
    {
        if(scenesPlayed == 1)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public int getStartSceneIndex()
    {
        return PlayerPrefs.GetInt("startScene");
    }
}

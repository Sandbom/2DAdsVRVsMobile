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

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
        AdvertySDK.Init();
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(allSounds[soundIndex], new Vector3(0,0,-50));
    }
}

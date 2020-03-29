using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(allSounds[soundIndex], new Vector3(0,0,-50));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{     
    public static SoundManagerScript Instance { get; set; }

    public AudioClip[] allSounds;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene(1);
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(allSounds[soundIndex], new Vector3(0, 0, 10));
    }
}

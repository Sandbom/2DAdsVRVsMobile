﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartSecondScene : MonoBehaviour
{
    public Material capMaterial;
    //public Object sceneToStart; funkar inte i VR av nån anledning


    public Text titleText;

    void Start()
    {
        if (StartUpHandlerScript.Instance.getStartSceneIndex() == 2)
        {
            titleText.text = "Blackbeards Tavern";
        }
        else titleText.text = "Redbeards Tavern";

    }
    void OnCollisionEnter(Collision col)
    {

        GameObject victim = this.GetComponent<Collider>().gameObject;

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);


        int soundIndex = Random.Range(0, 2);
        SoundManagerScript.Instance.PlaySound(soundIndex);


        if (!pieces[1].GetComponent<Rigidbody>())
        {

            pieces[1].AddComponent<Rigidbody>();
            MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
            pieces[0].GetComponent<MeshCollider>().enabled = false;
            temp.convex = true;
        }

        //Destroy(pieces[1], 2f);
        this.GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(Wait());
        if (StartUpHandlerScript.Instance.getStartSceneIndex() == 2)
        {
            SceneManager.LoadScene(3);
        }
        else SceneManager.LoadScene(2);
        //SceneManager.LoadScene("SceneWithoutAds");
        //SceneManager.LoadScene(sceneToStart.name); funkar inte i VR av nån anledning
       
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }

}


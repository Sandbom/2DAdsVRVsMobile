﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject[] fruits;

    private GameObject go;
    private Rigidbody temp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    IEnumerator SpawnFruit()
    {
        while (true)
        {
            int bombspawn = Random.Range(0, 10);

            if (bombspawn < 9)
            {

                go = Instantiate(fruits[Random.Range(0, fruits.Length - 1)]);
                temp = go.GetComponent<Rigidbody>();
            }
            else
            {
                Debug.Log("Spawn bomb");
                go = Instantiate(fruits[4]);
                temp = go.GetComponent<Rigidbody>();
            }
            temp.velocity = new Vector3(0f, 5f, -1.5f);
            temp.angularVelocity = new Vector3(0f, 0f, Random.Range(-5f, 5f));
            temp.useGravity = true;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            go.transform.position = pos;


            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

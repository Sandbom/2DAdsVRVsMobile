using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject[] fruits;

    private GameObject go;
    private Rigidbody temp;

    public float deltaSpawn;
    // Start is called before the first frame update
    void Start()
    {
        deltaSpawn = 1.1f;
        StartCoroutine(SpawnFruit());
        StartCoroutine(IncreaseSpawnSpeed());

    }

  

    IEnumerator SpawnFruit()
    {
      
        while (true)
        {
            int numberOfSpawns = Random.Range(1,10);
            if (numberOfSpawns <= 3) numberOfSpawns = 3;
            if (numberOfSpawns >= 8) numberOfSpawns = 2;
            else numberOfSpawns = 1;
            for (int i = 0; i < numberOfSpawns; i++)
            {
                int bombspawn = Random.Range(0, 10);

                if (bombspawn < 9)
                {

                    go = Instantiate(fruits[Random.Range(0, fruits.Length - 1)]);
                    temp = go.GetComponent<Rigidbody>();
                }
                else
                {
                    go = Instantiate(fruits[4]);
                    temp = go.GetComponent<Rigidbody>();
                }
                temp.velocity = new Vector3(0f, 5f, -1.5f);
                temp.angularVelocity = new Vector3(0f, 0f, Random.Range(-5f, 5f));
                temp.useGravity = true;
                Vector3 pos = transform.position;
                pos.x += Random.Range(-1f, 1f);
                go.transform.position = pos;

            }
          
             yield return new WaitForSeconds(deltaSpawn);

        }
    }

    IEnumerator IncreaseSpawnSpeed()
    {
        while (true)
        {
            Debug.Log(deltaSpawn);
        
            deltaSpawn -= 0.1f;
            Debug.Log(deltaSpawn);
            yield return new WaitForSeconds(30);
        }
    }

}

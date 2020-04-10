using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject[] fruits;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    IEnumerator SpawnFruit()
    {
        while (true)
        {
            //Debug.Log("Spawning");
            GameObject go = Instantiate(fruits[Random.Range(0, fruits.Length)]);
            Rigidbody temp = go.GetComponent<Rigidbody>();

            
            temp.velocity = new Vector3(0f, 5f, 0f);
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

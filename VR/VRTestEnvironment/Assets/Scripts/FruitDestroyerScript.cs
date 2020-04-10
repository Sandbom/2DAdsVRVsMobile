using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDestroyerScript : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }
  
    // Update is called once per frame
    /*void Update()
    {
        if (transform.position.y < -50f)
        {
            Debug.Log("destroy");
            Destroy(this.gameObject);
        }
    }*/
    


}

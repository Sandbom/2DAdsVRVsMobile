using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDestroyerScript : MonoBehaviour
{
    public bool isSliced;
    void Start()
    {
        isSliced = false;

        Destroy(this.gameObject, 2f);
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

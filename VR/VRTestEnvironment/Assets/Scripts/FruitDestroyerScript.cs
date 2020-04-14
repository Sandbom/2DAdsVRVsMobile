using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDestroyerScript : MonoBehaviour
{
    public bool isSliced;
    void Start()
    {
        isSliced = false;

        //Destroy(this.gameObject, 2f);
        Invoke("destroyFruit", 2f);
    }


    private void destroyFruit()
    {
        // If the meshcollider is active, that means the fruit was not sliced
        if (this.gameObject.GetComponent<MeshCollider>().enabled && this.gameObject.name != "BombPrefab(Clone)")
        {
            Debug.Log("Breaking streak because of missed fruit");
            GameManagerScript.Instance.BreakStreak();
        }
        Destroy(this.gameObject);
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

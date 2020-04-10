using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Rigidbody))]
public class SwordCutter : MonoBehaviour {

	public Material capMaterial;
    

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("In Collsion");
        GameObject victim = collision.collider.gameObject;
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        GameManagerScript.Instance.AddScore();

        if (!pieces[1].GetComponent<Rigidbody>())
            {
            
                pieces[1].AddComponent<Rigidbody>();
                //MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
                pieces[0].GetComponent<MeshCollider>().enabled = false;
                //temp.convex = true;
            }

        Destroy(pieces[1], 2f);
    }

}

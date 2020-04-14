using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Rigidbody))]
public class SwordCutter : MonoBehaviour {

	public Material capMaterial;
    public ParticleSystem yellowSplashEffect;
    public ParticleSystem redSplashEffect;
    private bool shouldSlice = true;


    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("In Collsion - Slicing a fruit");
        GameObject victim = collision.collider.gameObject;

        if (victim.name == "Tomato(Clone)" || victim.name == "Carrot(Clone)")
        {
            Instantiate(redSplashEffect, victim.transform);
            GameManagerScript.Instance.AddScore();
        }

        else if (victim.name == "Banana(Clone)" || victim.name == "Pineapple(Clone)")
        {
            Instantiate(yellowSplashEffect, victim.transform);
            GameManagerScript.Instance.AddScore();
        }

        else if (victim.name == "BombPrefab(Clone)")
        {
            GameManagerScript.Instance.BreakStreak();
            GameManagerScript.Instance.DecreaseScore();
            shouldSlice = false;
            Destroy(victim); // add some effect here for bomb cutting
        }

        if (shouldSlice)
        {
            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);


            int soundIndex = Random.Range(0, 2);
            SoundManagerScript.Instance.PlaySound(soundIndex);


            if (!pieces[1].GetComponent<Rigidbody>())
            {

                pieces[1].AddComponent<Rigidbody>();
                //MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
                pieces[0].GetComponent<MeshCollider>().enabled = false;
                //temp.convex = true;
            }

            Destroy(pieces[1], 2f);

        }
        shouldSlice = true;
    }

}

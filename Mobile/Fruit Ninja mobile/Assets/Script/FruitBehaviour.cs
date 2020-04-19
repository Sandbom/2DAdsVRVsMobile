using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{

    private const float GRAVITY = 2.0f;

    public bool isActive { set; get; }

    public float verticalVelocity;
    private float speed;
    private bool isSliced = false;
    private float rotationSpeed;
    public Material capMaterial;
    private bool soundPlayed = false;

    public void LauchFruit(float verticalVelocity, float xSpeed, float xStart)
    {
        isActive = true;
        speed = xSpeed;
        this.verticalVelocity = verticalVelocity;
        transform.position = new Vector3(xStart, 0, -1);
        isSliced = false;

        rotationSpeed = Random.Range(-180, 180);
    }

    private void Update()
    {

        if (isSliced)
        {
            if (!soundPlayed)
            {
                int soundIndex = Random.Range(0, 2);
                SoundManager.Instance.PlaySound(soundIndex);
                soundPlayed = true;
            }
            //Destroy(gameObject);
            Destroy(gameObject, 2.0f);
        }


        if (!isActive)
        {
            return;
        }

        verticalVelocity -= GRAVITY * Time.deltaTime;
        transform.position += new Vector3(speed, verticalVelocity, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed) *Time.deltaTime);

        if (transform.position.y < -1)
        {
            isActive = false;
            if (!isSliced && !(gameObject.tag == "Bomb"))
            {
                GameManager.Instance.LoseLifePoint();
            }
        }
    }

    public void Slice()
    {
        if (isSliced)
        {
            return;
        }

        if (verticalVelocity < 0.5)
        {
            verticalVelocity = 0.5f;
        }

        speed = speed * 0.5f;
        isSliced = true;

        if (!(gameObject.name == "BombPrefab(Clone)"))
        {
            GameManager.Instance.AddScore(1);
        }
    }
}

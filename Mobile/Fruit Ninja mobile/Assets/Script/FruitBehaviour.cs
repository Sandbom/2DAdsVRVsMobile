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

    public void LauchFruit(float verticalVelocity, float xSpeed, float xStart)
    {
        isActive = true;
        speed = xSpeed;
        this.verticalVelocity = verticalVelocity;
        transform.position = new Vector3(xStart, 0, 0);
        isSliced = false;

        rotationSpeed = Random.Range(-180, 180);
    }

    // Start is called before the first frame update
    /*private void Start()
    {
        LauchFruit(2.0f, 1, -1);
    }*/

    // Update is called once per frame
    private void Update()
    {

        if (isSliced)
        {

            int soundIndex = Random.Range(0, 2);
            SoundManager.Instance.PlaySound(soundIndex);
            Destroy(gameObject);
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
            if (!isSliced)
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

        GameManager.Instance.AddScore(1);
    }
}

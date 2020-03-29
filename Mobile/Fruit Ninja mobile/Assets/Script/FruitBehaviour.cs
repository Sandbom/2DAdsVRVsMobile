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

    public void LauchFruit(float verticalVelocity, float xSpeed, float xStart)
    {
        isActive = true;
        speed = xSpeed;
        this.verticalVelocity = verticalVelocity;
        transform.position = new Vector3(xStart, 0, 0);
        isSliced = false;
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
            Destroy(gameObject);
        }


        if (!isActive)
        {
            return;
        }

        verticalVelocity -= GRAVITY * Time.deltaTime;
        transform.position += new Vector3(speed, verticalVelocity, 0) * Time.deltaTime;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    public int inverter = 1;

    float movement;

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            movement = (Input.GetAxisRaw("Vertical"));
        }
        else
        {
            movement = (Input.GetAxisRaw("Vertical2"));
        }

        movement *= inverter;
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }


    // Reset is called once a player scores
    // Resets position and strips all powerup changes
    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.localScale = new Vector3(0.7f, 3f, 1f);
        inverter = 1;

        if (isPlayer1)
        {
            transform.position = new Vector3(-8f, 0f, 0f);
        }
        else
        {
            transform.position = new Vector3(8f, 0f, 0f);
        }
    }
}

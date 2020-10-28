using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public bool isCrazy = false;

    public GameObject ball, ball2;

    public AudioSource ping;

    void Start()
    {
        Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), ball2.GetComponent<Collider2D>(), true); // Balls won't collide with each other
    }

    // Update is called once per frame
    void Update()
    {
       

    }


    IEnumerator GoCrazy ()
    {
        while (isCrazy)
        {
            int n = Random.Range(0, 2) == 0 ? -1 : 1;
            // Set ball gravity in random direction
            rb.gravityScale = 3f * n;
            yield return new WaitForSeconds(0.75f);
            //Change ball gravity in opposite direction
            rb.gravityScale = -3f * n;
            yield return new WaitForSeconds(0.5f);
        }
        rb.gravityScale = 0f;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        ping.Play();
    }

    // Sends ball in random direction
    public void Launch()
    {
        //Set random velocity once round starts
        int x = Random.Range(0, 2) == 0 ? -1 : 1; 
        int y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
        
        if (isCrazy)
        {
            StartCoroutine(GoCrazy());
        }
    }

    // Reset ball position and properties
    public void Reset()
    {
        StopCoroutine(GoCrazy());
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        isCrazy = false;
        Launch();
    }
}

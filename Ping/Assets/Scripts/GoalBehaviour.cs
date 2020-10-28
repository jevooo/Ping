using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    public bool isPlayer1Goal;
   
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isPlayer1Goal)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
        }

    }
}

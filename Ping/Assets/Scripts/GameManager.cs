using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Balls")]
    public GameObject ball;
    public GameObject ball2;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;


    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject scoreCanvas;
    public GameObject player1Text;
    public GameObject player2Text;

    [Header("Round Info")]
    public GameObject roundCanvas;
    public GameObject roundText;
    public GameObject powerUps;
    public GameObject countdown;

    [Header("Victory Panel")]
    public GameObject victoryCanvas;
    public GameObject victoryText;

    [Header("SFX")]
    public AudioSource scoreFx;
    public AudioSource countdownFx;

    //---------------------------------------------------


    PauseScript pauseMenu;

    int player1Score;
    int player2Score;

    int round = 1;

    float timeLeft = 3.5f;
    bool inRound = false;

    bool two = false;
    bool one = false;



   void Start()
    {
        ball.SetActive(true);
        ball2.SetActive(false);
        pauseMenu = GetComponent<PauseScript>();
        victoryCanvas.SetActive(false);
    }

    void Update()
    {
        countdown.GetComponent<TextMeshProUGUI>().text = timeLeft.ToString("0");

        if (timeLeft > 0.5f)
        {
            pauseMenu.isEnabled = false; // Disable the pause menu

            //play sfx every second
            if (timeLeft < 2.5f && two == false)
            {
                countdownFx.Play();
                two = true;
            }
            if (timeLeft < 1.5f && one == false)
            {
                countdownFx.Play();
                one = true;
            }

            Time.timeScale = 0f;
            timeLeft -= Time.unscaledDeltaTime;
            // Game is paused during countdown
        }

        // Begin round
        if (timeLeft <= 0.5f)
        {
            if (!inRound)
            {
                two = false;
                one = false;

                Time.timeScale = 1f;
                roundCanvas.SetActive(false);
                ball.GetComponent<BallBehaviour>().Launch();
                //when the 2nd ball is active it will launch in the opposite direction of the first
                ball2.GetComponent<BallBehaviour>().Launch();
                ball2.GetComponent<BallBehaviour>().rb.velocity = ball.GetComponent<BallBehaviour>().rb.velocity * -1.0f;
                inRound = true;
                pauseMenu.isEnabled = true; 

            }
        }
    }



    public void Player1Scored()
    {
        pauseMenu.isEnabled = false; // Disable the pause menu
        scoreFx.Play();
        player1Score++;
        player1Text.GetComponent<TextMeshProUGUI>().text = player1Score.ToString();
        round++;
        roundText.GetComponent<TextMeshProUGUI>().text = "Round " + round.ToString();

        if (player1Score == 7 || player2Score == 7)
        {            
            // Display victory screen
            scoreCanvas.SetActive(false);
            victoryText.GetComponent<TextMeshProUGUI>().text = "Player 1 Victory";
            victoryCanvas.SetActive(true);
            ball.SetActive(false);
            ball2.SetActive(false);
        }
        else
        {
            Reset();

            ConfigureRound(round);

            // Start countdown 
            inRound = false;
            timeLeft = 3.5f;
            roundCanvas.SetActive(true);

        }
    }

    public void Player2Scored()
    {
        scoreFx.Play();
        pauseMenu.isEnabled = false;
        player2Score++;
        player2Text.GetComponent<TextMeshProUGUI>().text = player2Score.ToString();
        round++;
        roundText.GetComponent<TextMeshProUGUI>().text = "Round " + round.ToString();


        if (player2Score == 7 || player1Score == 7)
        {

            scoreCanvas.SetActive(false);
            victoryText.GetComponent<TextMeshProUGUI>().text = "Player 2 Victory";
            victoryCanvas.SetActive(true);
            ball.SetActive(false);
            ball2.SetActive(false);
        }
        else
        {
            Reset();

            ConfigureRound(round);

            inRound = false;
            timeLeft = 3.5f;
            roundCanvas.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    // Reset position of all moving objects
    void Reset()
    {
        player1Paddle.GetComponent<PaddleBehaviour>().Reset();
        player2Paddle.GetComponent<PaddleBehaviour>().Reset();
        ball.GetComponent<BallBehaviour>().Reset();
        ball2.GetComponent<BallBehaviour>().Reset();
        ball2.SetActive(false);

    }

    // Set powerups for round
    void ConfigureRound(int round)
    {
        int setter;

        // Single powerup setter, from rounds 2 to 3

        if (1 < round && round < 4)
        {
            setter = Random.Range(0, 4);
            
            if (setter == 0)
            {
                TwoBalls();
                powerUps.GetComponent<TextMeshProUGUI>().text = "Two Balls";
            }

            else if (setter == 1)
            {
                CrazyBall();
                powerUps.GetComponent<TextMeshProUGUI>().text = "Crazy Ball";
            }

            else if (setter == 2)
            {
                InvertedControls();
                powerUps.GetComponent<TextMeshProUGUI>().text = "Up is Down";
            }

            else if (setter == 3)
            {
                MiniPaddles();
                powerUps.GetComponent<TextMeshProUGUI>().text = "Mini Paddles";
            }
        }


        // Double powerup setter, from rounds 4 onward

        else if (round >= 4)
        {
            int[] n = { 0, 1, 2, 3};

            List<int> nums = new List<int>(n);

            setter = nums[Random.Range(0, 4)];

            nums.RemoveAt(setter);

            string tempText = "";

            // set first powerup 

            if (setter == 0)
            {
                TwoBalls();
                tempText = "Two Balls" + "\n+\n";
            }

            else if (setter == 1)
            {
                CrazyBall();
                tempText = "Crazy Ball" + "\n+\n";
            }

            else if (setter == 2)
            {
                InvertedControls();
                tempText = "Up is Down" + "\n+\n";
            }

            else if (setter == 3)
            {
                MiniPaddles();
                tempText = "Mini Paddles" + "\n+\n";
            }

          

            // set second powerup

            setter = nums[Random.Range(0, 3)];


            if (setter == 0)
            {
                TwoBalls();
                powerUps.GetComponent<TextMeshProUGUI>().text = tempText + "Two Balls";
            }

            else if (setter == 1)
            {
                CrazyBall();
                powerUps.GetComponent<TextMeshProUGUI>().text = tempText + "Crazy Ball";
            }

            else if (setter == 2)
            {
                InvertedControls();
                powerUps.GetComponent<TextMeshProUGUI>().text = tempText + "Up is Down";
            }

            else if (setter == 3)
            {
                MiniPaddles();
                powerUps.GetComponent<TextMeshProUGUI>().text = tempText + "Mini Paddles";
            }

        }
    }






    // powerup methods

    void GhostPaddles()
    {

    }

    void TwoBalls()
    {
        ball2.SetActive(true);
    }

    void CrazyBall()
    {
        ball.GetComponent<BallBehaviour>().isCrazy = true;
        ball2.GetComponent<BallBehaviour>().isCrazy = true;
    }

    void InvertedControls()
    {
        player1Paddle.GetComponent<PaddleBehaviour>().inverter = -1;
        player2Paddle.GetComponent<PaddleBehaviour>().inverter = -1;
    }

    void MiniPaddles()
    {
        Vector3 scale = new Vector3(0.7f, 2f, 1f);

        player1Paddle.transform.localScale = scale;
        player2Paddle.transform.localScale = scale;

    }

}

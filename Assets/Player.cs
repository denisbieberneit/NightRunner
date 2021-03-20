using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameCamera gameCam;

    public GameHandler gameHandler;
    public Text scoreText;
    public Text highscoreText;
    public Text coinText;

    public GameObject blood;

    public CharacterController2D characterController;
    public Rigidbody2D rigidBody2d;

    public float runSpeed;
    public float normalSpeed;
    public float maxSpeed;
    float horizontalMove = 0f;

    public Animator animator;

    bool isJumping = false;
    public bool afterJump = false;
    public bool inAir = false;
    public bool isDead = false;
    public bool afterSpeedUp = false;


    public GameObject ui;
    public GameObject deathScreen;

    public void Start()
    {
        ui.SetActive(true);
        deathScreen.SetActive(false);
    }

    private void Update()
    {

        if (!isDead)
        {
            int score = (int)(Mathf.Round(transform.position.x) / 2);
            int highscore = PlayerPrefs.GetInt("PlayerHighscore");

            horizontalMove = 1f * runSpeed;
            if (rigidBody2d.velocity.y < -0.1)
            {
                animator.SetBool("IsFalling", true);
            }
            else
            {
                animator.SetBool("IsFalling", false);
            }
            if (afterSpeedUp)
            {
                if (runSpeed > normalSpeed)
                {
                    float tempRun = runSpeed -1;
                    if (tempRun <= normalSpeed)
                    {
                        runSpeed = normalSpeed;
                        afterSpeedUp = false;
                        //animator.SetFloat("RunningSpeed", 1f); v0.6
                    }
                    else
                    {
                        runSpeed = tempRun;
                    }
                }
            }
            if (highscore < score)
            {
                PlayerPrefs.SetInt("PlayerHighscore", score);
            }
            //animator.SetFloat("RunningSpeed",  1 + runSpeed);
            scoreText.text = score+"m";
            coinText.text = "$" + PlayerPrefs.GetInt("PlayerCoins");
            highscoreText.text = PlayerPrefs.GetInt("PlayerHighscore")+"m";

        }
    }

    private void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        afterJump = true;
        inAir = false;
    }

    public void OnJumpButton()
    {
        animator.SetBool("IsJumping", true);
        isJumping = true;
        inAir = true; 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + 1);
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Respawn"))
        {
            gameHandler.LoadGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            isDead = true;
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            ui.SetActive(false);
            deathScreen.SetActive(true);
        }
    }

    public float GetRunspeed()
    {
        return runSpeed;
    }
    public void SetRunspeed(float newRunspeed)
    {
        runSpeed = newRunspeed;
    }
}

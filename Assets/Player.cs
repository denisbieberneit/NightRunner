using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameHandler gameHandler;
    public Text scoreText;
    public Text highscoreText;
    public Text coinText;

    public GameObject blood;

    public CharacterController2D characterController;
    public Rigidbody2D rigidBody2d;

    public float runSpeed = 0f;
    public float normalSpeed = 40f;
    float horizontalMove = 0f;

    public Animator animator;

    bool isJumping = false;
    bool afterJump = false;
    public bool isDead = false;


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
            if (afterJump)
            {
                if (runSpeed > normalSpeed)
                {
                    float tempRun = runSpeed - 1;
                    if (tempRun <= normalSpeed)
                    {
                        runSpeed = normalSpeed;
                        afterJump = false;
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
    }

    public void OnJumpButton()
    {
        animator.SetBool("IsJumping", true);
        isJumping = true;
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + 1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            gameHandler.LoadGame();
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            isDead = true;
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameHandler.LoadGame();
        }
    }
}

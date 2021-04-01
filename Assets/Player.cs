using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameCamera gameCam;

    public string playerName;

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

    LevelGenerator generator;

    public void Start()
    {
        if (ui != null)
        {
            ui.SetActive(true);
            deathScreen.SetActive(false);
        }
    }

    private void Update()
    {

        int score = (int)(Mathf.Round(transform.position.x) / 2);
        if (!isDead)
        {
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
            //animation speed
            animator.SetFloat("RunningSpeed", 1 + runSpeed/1200f);
            //end animation speed
            if (afterSpeedUp)
            {
                if (runSpeed > normalSpeed)
                {
                    float tempRun = runSpeed -.5f;
                    if (tempRun <= normalSpeed)
                    {
                        runSpeed = normalSpeed;
                        afterSpeedUp = false;
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
            if (coinText != null)
            {
                Color tmpColor = coinText.color;
                tmpColor.a = tmpColor.a - 0.003f;
                coinText.color = tmpColor;
                scoreText.text = score+"m";
                coinText.text = "$" + PlayerPrefs.GetInt("PlayerCoins");
                highscoreText.text = PlayerPrefs.GetInt("PlayerHighscore")+"m";
            }
        }
        PlayerPrefs.SetInt("PlayerScore", score);
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
        AudioManager.instance.Play("Jump");
        animator.SetBool("IsJumping", true);
        isJumping = true;
        inAir = true; 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + 1);
            Color color = coinText.color;
            color.a = 1;
            coinText.color = color;
            AudioManager.instance.Play("Coin");
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Respawn"))
        {
            gameHandler.LoadGame();
        }
        if (collider.gameObject.CompareTag("levelSpawn"))
        {
            generator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
            generator.spawnNext = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            isDead = true;
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            new WaitForSeconds(1);
            ui.SetActive(false);
            deathScreen.SetActive(true);
            if (PlayerPrefs.GetInt("PlayerScore") >= PlayerPrefs.GetInt("PlayerHighscore"))
            {
                AudioManager.instance.Play("Highscore");
            }
        }
        if (collision.gameObject.CompareTag("FallDeath"))
        {
            AudioManager.instance.Play("FallDeath");
            isDead = true;
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            ui.SetActive(false);
            deathScreen.SetActive(true);
            if (PlayerPrefs.GetInt("PlayerScore") >= PlayerPrefs.GetInt("PlayerHighscore"))
            {
                AudioManager.instance.Play("Highscore");
            }
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

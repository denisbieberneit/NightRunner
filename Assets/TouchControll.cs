using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControll : MonoBehaviour
{

    Player player;
    CharacterController2D controller;
    float jumpForce = 40f;
    float runForce = 40f;
    float forceMax = 1200f;

    //things for swipe control
    private Vector2 fp;
    private Vector2 lp;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        controller = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if ((fp.y - lp.y) < -120) // up swipe
                {
                    if (!player.isDead)
                    {
                        jumpForce = Mathf.Abs(fp.y - lp.y * 2.2f);
                        if (jumpForce > forceMax)
                        {
                            jumpForce = forceMax;
                        }
                        Debug.Log(jumpForce);
                        controller.addJumpForce = jumpForce;
                        player.OnJumpButton();
                    }
                }
            }
            if (!player.isDead)
            {
                if (!player.inAir)
                {
                    player.afterJump = false;
                    runForce += 25f;
                    if (runForce > forceMax)
                    {
                        runForce = forceMax;
                    }
                    if (player.GetRunspeed() < player.maxSpeed)
                    {
                        player.SetRunspeed(player.GetRunspeed() + (runForce / 2000));
                    }
                    if (player.GetRunspeed() > player.maxSpeed)
                    {
                        player.SetRunspeed(player.maxSpeed);
                    }
                }
                else
                {
                    runForce = 40;
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    if (!player.isDead)
                    {
                        player.afterSpeedUp = true;
                     }
                }
            }
        }
    }
}

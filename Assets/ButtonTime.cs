using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTime : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Player player;
    public CharacterController2D controller;
    public bool buttonPressed;
    float jumpForce = 40f;
    float forceMax = 1200f;


    private void Update()
    {
        if (!player.isDead)
        {
                if (buttonPressed && !player.inAir)
                {
                    player.afterJump = false;
                    jumpForce += 25f;
                    if (jumpForce > forceMax)
                    {
                        jumpForce = forceMax;
                    }
                    if (player.GetRunspeed() < player.maxSpeed)
                    {
                        player.SetRunspeed(player.GetRunspeed() + (jumpForce/2000));
                    }
                    if(player.GetRunspeed() > player.maxSpeed)
                    {
                        player.SetRunspeed(player.maxSpeed);
                    }
                }
                else
                {
                    jumpForce = 40;
                }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!player.isDead)
        {
            if (buttonPressed)
            {
                controller.addJumpForce = jumpForce;
                player.OnJumpButton();
                buttonPressed = false;
                controller.addJumpForce = jumpForce;
            }
        }
    }
}

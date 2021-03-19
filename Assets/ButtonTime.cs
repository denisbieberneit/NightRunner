using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTime : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Player player;
    public CharacterController2D controller;
    public bool buttonPressed;
    float jumpForce = 0f;
    float forceMax = 800f;


    private void Update()
    {
        if (!player.isDead)
        {
            if (buttonPressed)
            {
                jumpForce += 25f;
                if (jumpForce > forceMax)
                {
                    jumpForce = forceMax;
                }
                player.runSpeed = player.runSpeed + (jumpForce / 500);
            }
            else
            {
                if (player.runSpeed <= player.normalSpeed)
                {
                    player.runSpeed = player.normalSpeed;
                }else if (player.runSpeed > player.normalSpeed)
                {
                    player.runSpeed = player.runSpeed - (jumpForce / 1000);
                }
                jumpForce = 0;
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

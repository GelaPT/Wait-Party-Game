using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkFastPlayerController : PlayerController
{
    private float buttonTimer = 0.0f;

    private void Start()
    {
        if (Player.AI)
        {
            AIController = gameObject.AddComponent<ThinkFastAIController>();
            AIController.Player = (AIPlayer)Player;
        }
    }

    protected override void Update()
    {
        base.Update();

        buttonTimer += Time.deltaTime;

        if (buttonTimer < 1.0f) return;

        ThinkFastManager thinkFast = ThinkFastManager.Instance;

        if (InputManager.GetButton(Player, InputButton.Right) || InputManager.GetButton(Player, InputButton.B))
        {
            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Heart);

            if (thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Heart 
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotTriangle 
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotSquare)
            {
                Animator.SetTrigger("PressLeft");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }

            buttonTimer = 0.0f;
        }

        if (InputManager.GetButton(Player, InputButton.Down) || InputManager.GetButton(Player, InputButton.A))
        {
            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Triangle);

            if (thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Triangle 
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotSquare 
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotHeart)
            {
                Animator.SetTrigger("PressFront");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }

            buttonTimer = 0.0f;
        }

        if (InputManager.GetButton(Player, InputButton.Left) || InputManager.GetButton(Player, InputButton.X))
        {
            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Square);

            if (thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Square
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotTriangle 
                || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotHeart)
            {
                Animator.SetTrigger("PressRight");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }

            buttonTimer = 0.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkFastPlayerController : PlayerController
{
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

        if (InputManager.GetButton(Player, InputButton.Right) || InputManager.GetButton(Player, InputButton.B))
        {
            ThinkFastManager thinkFast = ThinkFastManager.Instance;

            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Square);

            if(thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Square || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotTriangle || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotHeart)
            {
                Animator.SetTrigger("PressRight");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }
        }

        if (InputManager.GetButton(Player, InputButton.Down) || InputManager.GetButton(Player, InputButton.A))
        {
            ThinkFastManager thinkFast = ThinkFastManager.Instance;

            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Triangle);

            if (thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Triangle || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotSquare || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotHeart)
            {
                Animator.SetTrigger("PressFront");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }
        }

        if (InputManager.GetButton(Player, InputButton.Left) || InputManager.GetButton(Player, InputButton.X))
        {
            ThinkFastManager thinkFast = ThinkFastManager.Instance;

            thinkFast.PlayerPressButton(Player, ThinkFastManager.ThinkFastButton.Heart);

            if (thinkFast.currentButton == ThinkFastManager.ThinkFastButton.Heart || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotTriangle || thinkFast.currentButton == ThinkFastManager.ThinkFastButton.NotSquare)
            {
                Animator.SetTrigger("PressLeft");
            }
            else
            {
                Animator.SetTrigger("PressWrong");
            }
        }
    }
}

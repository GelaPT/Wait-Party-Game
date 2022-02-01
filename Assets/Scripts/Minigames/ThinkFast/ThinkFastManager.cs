using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkFastManager : MGSingleton<ThinkFastManager>
{
    public enum ThinkFastButton : int
    {
        Triangle = 0,
        Heart = 1,
        Square = 2,
        NotTriangle = 3,
        NotHeart = 4,
        NotSquare = 5,
        None = 6
    }

    public RuntimeAnimatorController animatorController;

    public float[] playerTimer = new float[4];

    public ThinkFastButton currentButton = ThinkFastButton.None;

    private int round = 12;

    public bool isRound = false;

    public float roundTimer = 0.0f;
    public float roundSpan = 3.0f;
    public float breakTimer = 0.0f;
    public float breakSpan = 4.0f;

    private void Start()
    {
        base.Init();

        for (int i = 0; i < 4; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<ThinkFastPlayerController, CameraController>(playerSpawns[i], animatorController);
            playerTimer[i] = 0.0f;
        }
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < 4; i++) playerTimer[i] = Time.deltaTime;

        if (isRound)
        {
            roundTimer += Time.deltaTime;
            
            if(roundTimer > roundSpan)
            {
                Debug.Log("Acabou a ronda");

                breakTimer = 0.0f;

                currentButton = ThinkFastButton.None;

                // Implementar UI

                breakSpan = Random.Range(3.0f, 4.0f);

                isRound = false;
            }
        }
        else
        {
            breakTimer += Time.deltaTime;

            if(breakTimer > breakSpan)
            {
                Debug.Log("Começou a ronda");

                roundTimer = 0.0f;

                int button = Random.Range(0, 6);
                currentButton = (ThinkFastButton)button;

                // Implementar UI
                Debug.Log((ThinkFastButton)button);

                isRound = true;
                round--;
            }
        }

        if (round == 0)
        {
            var final = EndMinigame();

            foreach (var stat in final) Debug.Log(stat.player.ID + 1 + ": " + stat.place + " place with " + stat.points + " points!");

            MinigamesManager.Instance.UnloadMinigame();
        }
    }

    public void PlayerPressButton(Player player, ThinkFastButton button)
    {
        if (!isRound) return;
        if (playerTimer[player.ID] < 0.1f) return;

        Debug.Log(player.ID + "| " + button + " -> " + (int)((roundSpan - roundTimer) * 100));

        playerTimer[player.ID] = 0.0f;

        switch (currentButton)
        {
            case ThinkFastButton.Triangle:
                if (button == ThinkFastButton.Triangle) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
            case ThinkFastButton.Heart:
                if (button == ThinkFastButton.Heart) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
            case ThinkFastButton.Square:
                if (button == ThinkFastButton.Square) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
            case ThinkFastButton.NotTriangle:
                if (button != ThinkFastButton.Triangle) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
            case ThinkFastButton.NotHeart:
                if (button != ThinkFastButton.Heart) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
            case ThinkFastButton.NotSquare:
                if (button != ThinkFastButton.Square) stats[player.ID].points += (int)((roundSpan - roundTimer) * 100);
                break;
        }
    }
}
 
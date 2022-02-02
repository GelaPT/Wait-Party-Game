using UnityEngine;

public class DDRManager : MGSingleton<DDRManager>
{
    public RuntimeAnimatorController animatorController;

    private void Start()
    {
        base.Init();

        for (int i = 0; i < 4; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<DDRPlayerController, CameraController>(playerSpawns[i], animatorController);
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRManager : MonoBehaviour
{
    public RuntimeAnimatorController animatorController;

    public float ;

    public ThinkFastButton currentButton = ThinkFastButton.None;

    private int round = 12;

    public ThinkFastUI thinkFastUI;

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

            if (roundTimer > roundSpan)
            {
                breakTimer = 0.0f;

                currentButton = ThinkFastButton.None;

                thinkFastUI.ChangeIcon(currentButton);

                breakSpan = Random.Range(2.0f, 4.0f);

                isRound = false;
            }
        }
        else
        {
            breakTimer += Time.deltaTime;

            if (breakTimer > breakSpan)
            {
                roundTimer = 0.0f;

                int button = Random.Range(0, 6);
                currentButton = (ThinkFastButton)button;

                thinkFastUI.ChangeIcon(currentButton);

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

        int points = (int)((roundSpan - roundTimer) * 100);

        switch (currentButton)
        {
            case ThinkFastButton.Triangle:
                if (button == ThinkFastButton.Triangle)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }
                break;
            case ThinkFastButton.Heart:
                if (button == ThinkFastButton.Heart)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }
                break;
            case ThinkFastButton.Square:
                if (button == ThinkFastButton.Square)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }
                break;
            case ThinkFastButton.NotTriangle:
                if (button != ThinkFastButton.Triangle)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }
                break;
            case ThinkFastButton.NotHeart:
                if (button != ThinkFastButton.Heart)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }

                break;
            case ThinkFastButton.NotSquare:
                if (button != ThinkFastButton.Square)
                {
                    stats[player.ID].points += points;
                    thinkFastUI.UpdatePlayerScores(player.ID, points);
                }

                break;
        }
    }
}
*/
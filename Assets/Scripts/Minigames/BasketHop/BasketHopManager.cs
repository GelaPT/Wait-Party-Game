using System.Collections.Generic;
using UnityEngine;

public class BasketHopManager : MGSingleton<BasketHopManager>
{
    public Hop Hop;
    public BasketHopScriptableObject scriptableObject;
    public RuntimeAnimatorController animatorController;
    public BasketHopUI uiPanel;

    private float minigameTimer = 0.0f;

    protected override void Init()
    {
        base.Init();

        Physics.gravity *= 2;

        for (int i = 0; i < PlayerManager.Instance.Players.Length; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<BasketHopPlayerController, CameraController>(playerSpawns[i], animatorController);
            (PlayerManager.Instance.Players[i].PlayerController as BasketHopPlayerController).scriptableObject = scriptableObject;
        }
    }

    protected override void Update()
    {
        base.Update();

        for(int i = 0; i < 4; i++)
        {
            uiPanel.scores[i].SetText(stats[i].points < 10 ? "0" + stats[i].points : stats[i].points.ToString());
        }

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(60 - minigameTimer);

        int min = timeSpan.Minutes;
        int sec = timeSpan.Seconds;

        uiPanel.timer.SetText(min + "'" + sec + "''");

        minigameTimer += Time.deltaTime;
        if(minigameTimer > 60.0f)
        {
            minigameTimer = -500.0f;
            var final = EndMinigame();

            foreach (var stat in final)
            {
                Debug.Log(stat.player.ID + 1 + ": " + stat.place + " place with " + stat.points + " points!");
            }

            Time.timeScale = 0.0f;
        }
    }

    public override List<MinigameStats> EndMinigame()
    {
        Physics.gravity /= 2;
        return base.EndMinigame();
    }

    public void Score(Player player)
    {
        stats[player.ID].points++;
    }
}
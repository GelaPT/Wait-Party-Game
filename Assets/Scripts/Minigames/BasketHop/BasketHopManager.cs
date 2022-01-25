using UnityEngine;

public class BasketHopManager : MGSingleton<BasketHopManager>
{
    public BasketHopScriptableObject scriptableObject;
    public Transform[] playerSpawns;
    public RuntimeAnimatorController animatorController;

    private float minigameTimer = 0.0f;

    private void Start()
    {
        Init();

        Physics.gravity *= 2;

        for (int i = 0; i < PlayerManager.Instance.Players.Length; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<BasketHopPlayerController, CameraController>(playerSpawns[i], animatorController);
            (PlayerManager.Instance.Players[i].PlayerController as BasketHopPlayerController).scriptableObject = scriptableObject;
        }


    }

    private void Update()
    {
        minigameTimer += Time.deltaTime;
        if(minigameTimer > 60.0f)
        {
            minigameTimer = -500.0f;
            var final = EndMinigame();

            foreach (var stat in final)
            {
                Debug.Log(stat.player.ID + 1 + ": " + stat.place + " place with " + stat.points + " points!");
            }
        }
    }

    public void Score(Player player)
    {
        stats[player.ID].points++;
    }
}
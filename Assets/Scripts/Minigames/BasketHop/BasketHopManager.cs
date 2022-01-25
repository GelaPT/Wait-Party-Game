using UnityEngine;

public class BasketHopManager : MGSingleton<BasketHopManager>
{
    public BasketHopScriptableObject scriptableObject;
    public Transform[] playerSpawns;
    public RuntimeAnimatorController animatorController;

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

    public void Score(Player player)
    {
        stats[player.ID].points++;
    }
}
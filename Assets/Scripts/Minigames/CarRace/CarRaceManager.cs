using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceManager : MGSingleton<CarRaceManager>
{
    public Transform[] Checkpoints;
    public bool gameHasBegun = false;

    public GameObject lapPrefab;
    public Vector3 lapOffset;

    public int finished = 0;
    public int finishBonus = 4;
    public float finishTimer = 0.0f;

    private void Start()
    {
        base.Init();

        Physics.gravity *= 8.0f;

        for(int i = 0; i < 4; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<CarRacePlayerController, CameraController>(gameObject.transform, null);
            (PlayerManager.Instance.Players[i].PlayerController as CarRacePlayerController).car = playerSpawns[i].gameObject;
            (PlayerManager.Instance.Players[i].PlayerController as CarRacePlayerController).carRb = playerSpawns[i].GetComponent<Rigidbody>();
        }

        Invoke("StartGame", 3.0f);
    }

    protected override void Update()
    {
        if (ended) return;

        base.Update();

        if (finished == 0) return;

        finishTimer += Time.deltaTime;

        if(finished == 4)
        {
            UIManager.Instance.ShowResults(EndMinigame());
            Physics.gravity /= 8.0f;
            ended = true;
        }

        if (finishTimer > 30.0f)
        {
            UIManager.Instance.ShowResults(EndMinigame());
            Physics.gravity /= 8.0f;
            ended = true;
        }
    }

    private void StartGame()
    {
        gameHasBegun = true;
    }

    public void ResetCar(GameObject car)
    {
        for(int i = 0; i < 4; i++)
        {
            if(car.transform == playerSpawns[i])
            {
                CarRacePlayerController crpc = PlayerManager.Instance.Players[i].PlayerController as CarRacePlayerController;
                crpc.ResetCar(Checkpoints[crpc.currentCheckpoint]);
            }
        }
    }

    public void DrawLap(Player player, int lap)
    {
        GameObject lapText = Instantiate(lapPrefab, Checkpoints[0].position + lapPrefab.transform.localPosition, lapPrefab.transform.rotation);

        switch(lap)
        {
            case 1:
                lapText.GetComponent<CarRaceLapText>().lapText.SetText("2nd Lap");
                break;
            case 2:
                lapText.GetComponent<CarRaceLapText>().lapText.SetText("Final Lap");
                break;
            case 3:
                lapText.GetComponent<CarRaceLapText>().lapText.SetText("Finished");
                finished++;
                stats[player.ID].points += finishBonus--;
                break;
            default:
                break;
        }
    }

    public void NextCheckpoint(GameObject car, int checkpoint)
    {
        foreach (Player player in PlayerManager.Instance.Players)
        {
            if ((player.PlayerController as CarRacePlayerController).car == car)
            {
                if((player.PlayerController as CarRacePlayerController).NextCheckpoint(checkpoint))
                {
                    if ((player.PlayerController as CarRacePlayerController).currentLap >= 3) return;
                    stats[player.ID].points++;
                }
            }
        }
    }
}

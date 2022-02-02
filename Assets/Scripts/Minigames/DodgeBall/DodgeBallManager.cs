using UnityEngine;

public class DodgeBallManager : MGSingleton<DodgeBallManager>
{
    public RuntimeAnimatorController animatorController;

    public GameObject acornHand;
    public GameObject acornWorld;

    public int teamOnePlayerAcorn = 0;
    public int teamTwoPlayerAcorn = 1;

    private void Start()
    {
        base.Init();

        for(int i = 0; i < 4; i++)
        {
            PlayerManager.Instance.Players[i].Spawn<DodgeBallPlayerController, CameraController>(playerSpawns[i].transform, animatorController);
            (PlayerManager.Instance.Players[i].PlayerController as DodgeBallPlayerController).acornHand = acornHand;
            (PlayerManager.Instance.Players[i].PlayerController as DodgeBallPlayerController).acornWorld = acornWorld;
        }
        (PlayerManager.Instance.Players[0].PlayerController as DodgeBallPlayerController).team = 0;
        (PlayerManager.Instance.Players[1].PlayerController as DodgeBallPlayerController).team = 1;
        (PlayerManager.Instance.Players[2].PlayerController as DodgeBallPlayerController).team = 0;
        (PlayerManager.Instance.Players[3].PlayerController as DodgeBallPlayerController).team = 1;

        Invoke("GiveBalls", 3.0f);
    }

    private void GiveBalls()
    {
        GiveBall(0);
        GiveBall(1);
    }

    protected override void Update()
    {
        bool t1Alive = false;
        bool t2Alive = false;

        if (PlayerManager.Instance.Players[0] != null) t1Alive = true;
        if (PlayerManager.Instance.Players[1] != null) t2Alive = true;
        if (PlayerManager.Instance.Players[2] != null) t1Alive = true;
        if (PlayerManager.Instance.Players[3] != null) t2Alive = true;

        if(!t1Alive)
        {
            stats[1].points++;
            stats[3].points++;
        }
        else if(!t2Alive)
        {
            stats[0].points++;
            stats[2].points++;
        }
    }

    public void GiveBall(int team)
    {
        switch (team)
        {
            case 0:
                if (teamOnePlayerAcorn == 0 && PlayerManager.Instance.Players[0] == null) teamOnePlayerAcorn = 2;
                if (teamOnePlayerAcorn == 2 && PlayerManager.Instance.Players[2] == null) teamOnePlayerAcorn = 0;
                if ((PlayerManager.Instance.Players[teamOnePlayerAcorn].PlayerController as DodgeBallPlayerController).balls == 0) (PlayerManager.Instance.Players[teamOnePlayerAcorn].PlayerController as DodgeBallPlayerController).PickUpBall();
                else (PlayerManager.Instance.Players[teamOnePlayerAcorn].PlayerController as DodgeBallPlayerController).balls++;
                teamOnePlayerAcorn = teamOnePlayerAcorn == 0 ? 2 : 0;
                break;
            case 1:
                if (teamOnePlayerAcorn == 1 && PlayerManager.Instance.Players[1] == null) teamOnePlayerAcorn = 3;
                if (teamOnePlayerAcorn == 3 && PlayerManager.Instance.Players[3] == null) teamOnePlayerAcorn = 1;
                if ((PlayerManager.Instance.Players[teamTwoPlayerAcorn].PlayerController as DodgeBallPlayerController).balls == 0) (PlayerManager.Instance.Players[teamTwoPlayerAcorn].PlayerController as DodgeBallPlayerController).PickUpBall();
                else (PlayerManager.Instance.Players[teamTwoPlayerAcorn].PlayerController as DodgeBallPlayerController).balls++;
                teamTwoPlayerAcorn = teamTwoPlayerAcorn == 1 ? 3 : 1;
                break;
        }
    }
}
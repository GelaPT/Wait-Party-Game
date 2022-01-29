using UnityEngine;

public class CannonDodgeManager : MGSingleton<CannonDodgeManager>
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    private int wave = 1;
    private float waveTimer = 0.0f;
    [SerializeField] private Transform cannonSpawn;
    [SerializeField] private GameObject cannon;

    private bool[] alivePlayer = new bool[4];

    private void Start()
    {
        base.Init();
        
        for(int i = 0; i < 4; i++)
        {
            alivePlayer[i] = true;
            PlayerManager.Instance.Players[i].Spawn<CannonDodgePlayerController, CameraController>(playerSpawns[i], animatorController);
        }
    }

    protected override void Update()
    {
        base.Update();

        waveTimer += Time.deltaTime;
        if(waveTimer > 4.0f)
        {
            for(int i = 0; i < wave; i++)
            {
                Invoke("SpawnCannon", Random.Range(0.0f, 1.0f));
            }

            waveTimer = 0.0f;

            wave++;
        }

        int deadPlayers = 0;
        foreach(bool alive in alivePlayer)
        {
            if(alive == false) deadPlayers++;
        }

        if(deadPlayers == 3)
        {
            var final = EndMinigame();

            foreach (var stat in final)
            {
                Debug.Log(stat.player.ID + 1 + ": " + stat.place + " place with " + stat.points + " points!");
            }

            Time.timeScale = 0.0f;
        }
    }

    private void SpawnCannon()
    {
        float angle = Random.Range(0, 360);
        Vector2 angleVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector3 position = cannonSpawn.transform.position + new Vector3(angleVector.x, 0, angleVector.y) * 12f;
        GameObject c = Instantiate(cannon, position, Quaternion.LookRotation(position - cannonSpawn.transform.position), cannonSpawn);
        c.transform.Rotate(new Vector3(0, Random.Range(70, 121), 0));
    }

    public void Kill(CannonDodgePlayerController player)
    {
        player.Kill();

        alivePlayer[player.Player.ID] = false;

        for(int i = 0; i < 4; i++)
        {
            if (!alivePlayer[i]) continue;
            stats[i].points++;
        }
    }
}
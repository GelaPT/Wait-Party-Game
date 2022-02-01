using UnityEngine;

public class CarRaceAIController : AIController
{
    public CarRacePlayerController crpc;

    private float randomAngle;

    public CarRaceAIController(Player player) : base(player) { }

    private void Start()
    {
        randomAngle = Random.Range(15, 20);
    }

    public override void UpdateAI()
    {
        int nextCheckpoint = crpc.currentCheckpoint + 1;

        if(crpc.currentCheckpoint == 29) nextCheckpoint = 0;

        Player.PressButton(InputButton.A);

        float angle = Vector3.SignedAngle(crpc.car.transform.forward, CarRaceManager.Instance.Checkpoints[nextCheckpoint].transform.forward, Vector3.up);



        if (angle > randomAngle)
        {
            Player.MoveAxis(InputAxis.Left, InputAxisDir.E);
        }
        else if (angle < -randomAngle)
        {
            Player.MoveAxis(InputAxis.Left, InputAxisDir.W);
        }
        else
        {
            Player.MoveAxis(InputAxis.Left, InputAxisDir.None);
        }
    }
}

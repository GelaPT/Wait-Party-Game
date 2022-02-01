using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRacePlayerController : PlayerController
{
    public GameObject car;
    public Rigidbody carRb;
    public int currentLap = 0;

    public int currentCheckpoint = 0;

    public float speed = 25.0f;
    public float turnSpeed = 280.0f;
    private float accelTimer = 0.0f;
    private float accelSpan = 2.0f;
    private float deaccelTimer = 0.0f;
    private float deaccelSpan = 2.0f;

    private void Start()
    {
        currentLap = 0;
        currentCheckpoint = 0;

        if (Player.AI)
        {
            AIController = gameObject.AddComponent<CarRaceAIController>();
            AIController.Player = Player as AIPlayer;
            (AIController as CarRaceAIController).crpc = (Player.PlayerController as CarRacePlayerController);
            speed = speed - 1.0f;
        }
    }

    protected override void Update()
    {
        base.Update();

        bool isGrounded = false;

        Collider[] collisions = Physics.OverlapSphere(car.transform.position, 0.2f);
        foreach(Collider collision in collisions)
        {
            if(collision.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        if(BaseVelocity == Vector3.zero)
        {
            accelTimer = 0.0f;
            deaccelTimer += Time.deltaTime;
        }
        else
        {
            deaccelTimer = 0.0f;
            accelTimer += Time.deltaTime;
        }

        if(!isGrounded)
        {
            BaseVelocity = Vector3.zero;
            BaseAngularVelocity = Vector3.zero;
            return;
        }

        if (InputManager.GetAxisDir(Player, InputAxis.Left, InputAxisDir.W))
        {
            BaseAngularVelocity = -turnSpeed * Vector3.up;
        }
        else if (InputManager.GetAxisDir(Player, InputAxis.Left, InputAxisDir.E))
        {
            BaseAngularVelocity = turnSpeed * Vector3.up;
        }
        else BaseAngularVelocity = Vector3.zero;

        if (InputManager.GetButtons(Player, new InputButton[] { InputButton.A, InputButton.B })) BaseVelocity = Vector3.zero;
        else if (InputManager.GetButton(Player, InputButton.A)) BaseVelocity = speed * car.transform.forward;
        else if (InputManager.GetButton(Player, InputButton.B)) BaseVelocity = -speed * car.transform.forward;
        else BaseVelocity = Vector3.zero;

        AngularVelocity = BaseAngularVelocity;
    }

    protected override void FixedUpdate()
    {
        float lastY = carRb.velocity.y;

        if (BaseVelocity == Vector3.zero)
        {
            Velocity = Vector3.Lerp(Velocity, Vector3.zero, deaccelTimer / deaccelSpan);
        } 
        else
        {
            Velocity = Vector3.Lerp(Velocity, BaseVelocity, accelTimer / accelSpan);
        }
        
        carRb.velocity = new Vector3(0, lastY, 0);

        carRb.velocity = Velocity + Vector3.up * carRb.velocity.y;

        float angle = Vector2.SignedAngle(new Vector2(car.transform.right.x, car.transform.right.z), new Vector2(Velocity.x, Velocity.z));

        car.transform.Rotate(carRb.velocity.magnitude / speed * Time.deltaTime * (angle > 0 ? 1 : -1) * AngularVelocity);
        carRb.angularVelocity = Vector3.zero;
    }

    public void ResetCar(Transform checkpoint)
    {
        car.transform.position = checkpoint.position + Vector3.up;
        car.transform.rotation = checkpoint.rotation;

        Velocity = Vector3.zero;
        AngularVelocity = Vector3.zero;
        carRb.velocity = Vector3.zero;
        carRb.angularVelocity = Vector3.zero;
    }

    public bool NextCheckpoint(int checkpoint)
    {
        if (currentCheckpoint == checkpoint) return false;

        if(currentCheckpoint == 29 && checkpoint == 0)
        {
            currentLap++;
            CarRaceManager.Instance.DrawLap(Player, currentLap);
            currentCheckpoint = checkpoint;
            return true;
        }

        if(checkpoint == currentCheckpoint + 1)
        {
            currentCheckpoint = checkpoint;
            return true;
        }

        ResetCar(CarRaceManager.Instance.Checkpoints[currentCheckpoint].transform);
        return false;
    }
}

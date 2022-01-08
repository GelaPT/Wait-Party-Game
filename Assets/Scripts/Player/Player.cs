using UnityEngine.InputSystem;

[System.Serializable]
public class Player
{
    public Gamepad gamepad;

    public Character Character { get; private set; }

    public PlayerController PlayerController { get; private set; }
    public CameraController CameraController { get; private set; }

    public Player(Gamepad gamepad)
    {
        this.gamepad = gamepad;
    }
   
    //Deactivate / Activate / Update

    public void Respawn(PlayerController playerController, CameraController cameraController)
    {
        PlayerController = playerController;
        CameraController = cameraController;
    }
}
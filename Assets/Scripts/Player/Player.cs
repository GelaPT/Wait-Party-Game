using UnityEngine.InputSystem;

[System.Serializable]
public class Player
{
    public Gamepad gamepad;
    public GamepadScheme gamepadScheme = GamepadScheme.Xbox;

    public PlayerController PlayerController { get; private set; }
    public CameraController CameraController { get; private set; }

    public Player(Gamepad gamepad)
    {
        this.gamepad = gamepad;
    }

    /*
    public Player(int player, ParsecGaming.Parsec.ParsecGuest guest)
    {
        ID = player;
        assignedGuest = guest;
        parsec = true;
        ParsecUnity.ParsecInput.AssignGuestToPlayer(assignedGuest, ID);
    }*/

    //Deactivate / Activate / Update

    public void Respawn(PlayerController playerController, CameraController cameraController)
    {
        PlayerController = playerController;
        CameraController = cameraController;
    }
}
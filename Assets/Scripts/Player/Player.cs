using UnityEngine.InputSystem;

public class Player
{
    private static int currentId = 0;
    public int id;
    public Gamepad gamepad;
    public GamepadScheme gamepadScheme;
    public bool parsec = false;
    public ParsecGaming.Parsec.ParsecGuest assignedGuest;

    public PlayerController controller;
    public CameraController cameraController;

    public Player(Gamepad gamepad)
    {
        id = currentId++;
        this.gamepad = gamepad;

        //controller = BasicController

    }

    //Deactivate / Activate / Update
}
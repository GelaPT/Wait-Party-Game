using UnityEngine.InputSystem;

[System.Serializable]
public class Player
{
    public int id;
    public Gamepad gamepad;
    public GamepadScheme gamepadScheme;
    public bool parsec = false;
    public ParsecGaming.Parsec.ParsecGuest assignedGuest;
    public int gamepadNumber;

    public PlayerController controller;
    public CameraController cameraController;

    public Player()
    {

    }

    public Player(Gamepad gamepad)
    {
        this.gamepad = gamepad;

        //controller = BasicController

    }

    public Player(int player, ParsecGaming.Parsec.ParsecGuest guest)
    {
        id = player;
        assignedGuest = guest;
        parsec = true;
        ParsecUnity.ParsecInput.AssignGuestToPlayer(assignedGuest, id);
    }

    //Deactivate / Activate / Update
}
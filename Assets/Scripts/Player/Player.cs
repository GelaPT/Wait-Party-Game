using UnityEngine.InputSystem;

[System.Serializable]
public class Player
{
    public int CurrentID { get; private set; } = 0;
    public int ID { get; private set; }
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
        ID = CurrentID++;

        //controller = BasicController

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
}
using UnityEngine.InputSystem;

public class Player
{
    private static int currentId = 0;
    public int id;
    public Gamepad gamepad;

    public Player(Gamepad gamepad)
    {
        id = currentId++;
        this.gamepad = gamepad;

    }
}
using UnityEngine;

public enum GamepadScheme { Xbox, Nintendo }

public enum InputButton : int
{
    A = 0,
    B = 1,
    Y = 2,
    X = 3,
    Start = 4,
    Select = 5,
    Up = 6,
    Down = 7,
    Left = 8,
    Right = 9,
    RB = 10, // Right Button // Shoulder
    RT = 11, // Right Trigger
    RS = 12, // Right Stick
    LB = 13, // Left Button // Shoulder
    LT = 14, // Left Trigger
    LS = 15  // Left Stick
}

public class InputManager : Singleton<InputManager>
{
    private Vector2 GetAxis(Player player, string axis)
    {
        return axis switch
        {
            "right" => player.gamepad.rightStick.ReadValue(),
            "left" => player.gamepad.rightStick.ReadValue(),
            _ => Vector2.zero
        };
    }

    public bool GetButton(Player player, InputButton button) => button switch
    {
        InputButton.A => player.gamepadScheme == GamepadScheme.Xbox ? player.gamepad.buttonSouth.isPressed : player.gamepad.buttonEast.isPressed,
        InputButton.B => player.gamepadScheme == GamepadScheme.Xbox ? player.gamepad.buttonEast.isPressed : player.gamepad.buttonSouth.isPressed,
        InputButton.Y => player.gamepadScheme == GamepadScheme.Xbox ? player.gamepad.buttonNorth.isPressed : player.gamepad.buttonWest.isPressed,
        InputButton.X => player.gamepadScheme == GamepadScheme.Xbox ? player.gamepad.buttonWest.isPressed : player.gamepad.buttonNorth.isPressed,
        InputButton.Start => player.gamepad.startButton.isPressed,
        InputButton.Select => player.gamepad.selectButton.isPressed,
        InputButton.Up => player.gamepad.dpad.up.isPressed,
        InputButton.Down => player.gamepad.dpad.down.isPressed,
        InputButton.Left => player.gamepad.dpad.left.isPressed,
        InputButton.Right => player.gamepad.dpad.right.isPressed,
        InputButton.RB => player.gamepad.rightShoulder.isPressed,
        InputButton.RT => player.gamepad.rightTrigger.isPressed,
        InputButton.RS => player.gamepad.rightStickButton.isPressed,
        InputButton.LB => player.gamepad.leftShoulder.isPressed,
        InputButton.LT => player.gamepad.leftTrigger.isPressed,
        InputButton.LS => player.gamepad.leftStickButton.isPressed,
        _ => throw new System.NotImplementedException()
    };
}
using UnityEngine;
using ParsecUnity;
using ParsecGaming;

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
    LB = 13, // Left Button
    LT = 14, // Left Trigger
    LS = 15  // Left Stick
}

public class InputManager : Singleton<InputManager>
{

    public Vector2 RightAxis { get; private set; }
    public Vector2 LeftAxis { get; private set; }


    private void Start()
    {
        
    }

    public bool GetButton(Player player, InputButton button)
    {
        // If the player is a Parsec client
        if (player.parsec)
        {
            return button switch
            {
                InputButton.A => player.gamepadScheme == GamepadScheme.Xbox ? ParsecInput.GetButton(2, "joystick button 0") : ParsecInput.GetButton(2, "joystick button 1"),
                InputButton.B => player.gamepadScheme == GamepadScheme.Xbox ? ParsecInput.GetButton(2, "joystick button 1") : ParsecInput.GetButton(2, "joystick button 0"),
                InputButton.X => player.gamepadScheme == GamepadScheme.Xbox ? ParsecInput.GetButton(2, "joystick button 2") : ParsecInput.GetButton(2, "joystick button 3"),
                InputButton.Y => player.gamepadScheme == GamepadScheme.Xbox ? ParsecInput.GetButton(2, "joystick button 3") : ParsecInput.GetButton(2, "joystick button 2"),
                InputButton.LB => ParsecInput.GetButton(2, "joystick button 4"),
                InputButton.RB => ParsecInput.GetButton(2, "joystick button 5"),
                InputButton.Select => ParsecInput.GetButton(2, "joystick button 6"),
                InputButton.Start => ParsecInput.GetButton(2, "joystick button 7"),
                InputButton.LS => ParsecInput.GetButton(2, "joystick button 8"),
                InputButton.RS => ParsecInput.GetButton(2, "joystick button 9"),
                InputButton.Right => ParsecInput.GetButton(2, "6 axis"),
                InputButton.Left => ParsecInput.GetButton(2, "6 axis"),
                InputButton.Up => ParsecInput.GetButton(2, "7 axis"),
                InputButton.Down => ParsecInput.GetButton(2, "7 axis"),
                InputButton.RT => ParsecInput.GetAxis(2, "3 axis") < 0,
                InputButton.LT => ParsecInput.GetAxis(2, "3 axis") > 0,
                _ => throw new System.NotImplementedException()
            };
        }

        return button switch
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
}
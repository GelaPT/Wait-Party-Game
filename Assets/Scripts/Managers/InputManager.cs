using UnityEngine;

public enum InputAxis : int
{
    Left = 0,
    Right = 1,
}

public enum InputAxisDir : int
{
    None = -1, // None
    N = 0, // North
    NE = 1, // North East
    E = 2, // East
    SE = 3, // South East
    S = 4, // South
    SW = 5, // South West
    W = 6, // West
    NW = 7 // North West
}

public enum InputButton : int
{
    A = 0,
    B = 1,
    Y = 2,
    X = 3,
    Start = 4,
    Select = 5,
    Up = 6, // D-Pad Up
    Down = 7, // D-Pad Down
    Left = 8, // D-Pad Left
    Right = 9, // D-Pad Right
    RB = 10, // Right Button // Shoulder
    RT = 11, // Right Trigger
    RS = 12, // Right Stick
    LB = 13, // Left Button // Shoulder
    LT = 14, // Left Trigger
    LS = 15  // Left Stick
}

public static class InputManager
{
    #region Axis

    private static float[,,] playerAxisTimer = new float[4, 2, 8];

    public static Vector2 GetAxis(Player player, InputAxis axis)
    {
        return axis switch
        {
            InputAxis.Left => player.AI ? (player as AIPlayer).FakeInputAxis[(int)axis] : player.Gamepad.leftStick.ReadValue(),
            InputAxis.Right => player.AI ? (player as AIPlayer).FakeInputAxis[(int)axis] : player.Gamepad.rightStick.ReadValue(),
            _ => Vector2.zero
        };
    }
    public static Vector2 GetAxis(int playerID, InputAxis axis)
    {
        return GetAxis(PlayerManager.Instance.Players[playerID], axis);
    }

    public static InputAxisDir GetAxisDir(Player player, InputAxis axis)
    {
        return GetAxisDir(PlayerManager.Instance.GetPlayerID(player), axis);
    }
    public static InputAxisDir GetAxisDir(int playerID, InputAxis axis)
    {
        Vector2 axisDir = GetAxis(playerID, axis);

        if (axisDir.magnitude < 0.15f) return InputAxisDir.None;

        axisDir.Normalize();

        float theta = Vector2.SignedAngle(Vector2.right, axisDir);
        theta = theta < 0.0f ? 360.0f + theta : theta; // 0 to 360

        float dir = theta / 45.0f;

        if (dir < 0.5f) return InputAxisDir.E;
        else if (dir < 1.5f) return InputAxisDir.NE;
        else if (dir < 2.5f) return InputAxisDir.N;
        else if (dir < 3.5f) return InputAxisDir.NW;
        else if (dir < 4.5f) return InputAxisDir.W;
        else if (dir < 5.5f) return InputAxisDir.SW;
        else if (dir < 6.5f) return InputAxisDir.S;
        else if (dir < 7.5f) return InputAxisDir.SE;
        else return InputAxisDir.E;
    }

    public static bool GetAxisDir(Player player, InputAxis axis, InputAxisDir axisDir)
    {
        return GetAxisDir(PlayerManager.Instance.GetPlayerID(player), axis, axisDir);
    }
    public static bool GetAxisDir(int playerID, InputAxis axis, InputAxisDir axisDir)
    {
        return GetAxisDir(playerID, axis) == axisDir;
    }

    public static bool GetAxisDir(Player player, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        return GetAxisDir(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, delay);
    }
    public static bool GetAxisDir(int playerID, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        if (GetAxisDir(playerID, axis) == axisDir)
        {
            return CanMoveAgain(playerID, axis, axisDir, delay);
        }

        return false;
    }

    public static bool CanMoveAgain(Player player, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        return CanMoveAgain(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, delay);
    }
    public static bool CanMoveAgain(int playerID, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        if (Time.time - playerAxisTimer[playerID, (int)axis, (int)axisDir] > delay)
        {
            playerAxisTimer[playerID, (int)axis, (int)axisDir] = Time.time;
            return true;
        }

        return false;
    }

    public static bool CanMoveAgainRaw(Player player, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        return CanMoveAgainRaw(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, delay);
    }
    public static bool CanMoveAgainRaw(int playerID, InputAxis axis, InputAxisDir axisDir, float delay)
    {
        return Time.time - playerAxisTimer[playerID, (int)axis, (int)axisDir] > delay;
    }

    public static void AddAxisTimer(Player player, InputAxis axis, InputAxisDir axisDir, float time)
    {
        AddAxisTimer(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, time);
    }
    public static void AddAxisTimer(int playerID, InputAxis axis, InputAxisDir axisDir, float time)
    {
        playerAxisTimer[playerID, (int)axis, (int)axisDir] = Time.time + time;
    }

    #endregion Axis

    #region Gamepad

    private static float[,] playerButtonTimer = new float[4, 16];
    public static bool GetButton(Player player, InputButton button) => button switch
    {
        InputButton.A => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.buttonSouth.isPressed,
        InputButton.B => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.buttonEast.isPressed,
        InputButton.Y => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.buttonNorth.isPressed,
        InputButton.X => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.buttonWest.isPressed,
        InputButton.Start => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.startButton.isPressed,
        InputButton.Select => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.selectButton.isPressed,
        InputButton.Up => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.dpad.up.isPressed,
        InputButton.Down => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.dpad.down.isPressed,
        InputButton.Left => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.dpad.left.isPressed,
        InputButton.Right => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.dpad.right.isPressed,
        InputButton.RB => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.rightShoulder.isPressed,
        InputButton.RT => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.rightTrigger.isPressed,
        InputButton.RS => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.rightStickButton.isPressed,
        InputButton.LB => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.leftShoulder.isPressed,
        InputButton.LT => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.leftTrigger.isPressed,
        InputButton.LS => player.AI ? (player as AIPlayer).FakeInput[(int)button] : player.Gamepad.leftStickButton.isPressed,
        _ => throw new System.NotImplementedException()
    };
    public static bool GetButton(int playerID, InputButton button)
    {
        return GetButton(PlayerManager.Instance.Players[playerID], button);
    }

    public static bool GetButton(Player player, InputButton button, float delay)
    {
        if (GetButton(player, button))
        {
            return CanPressAgain(player, button, delay);
        }

        return false;
    }
    public static bool GetButton(int playerID, InputButton button, float delay)
    {
        if (GetButton(playerID, button))
        {
            return CanPressAgain(playerID, button, delay);
        }

        return false;
    }

    public static bool CanPressAgain(Player player, InputButton button, float delay)
    {
        return CanPressAgain(PlayerManager.Instance.GetPlayerID(player), button, delay);
    }
    public static bool CanPressAgain(int playerID, InputButton button, float delay)
    {
        if (Time.time - playerButtonTimer[playerID, (int)button] > delay)
        {
            playerButtonTimer[playerID, (int)button] = Time.time;
            return true;
        }
        return false;
    }

    public static bool CanPressAgainRaw(Player player, InputButton button, float delay)
    {
        return CanPressAgainRaw(PlayerManager.Instance.GetPlayerID(player), button, delay);
    }
    public static bool CanPressAgainRaw(int playerID, InputButton button, float delay)
    {
        return Time.time - playerButtonTimer[playerID, (int)button] > delay;
    }

    public static void AddButtonTimer(Player player, InputButton button, float time)
    {
        AddButtonTimer(PlayerManager.Instance.GetPlayerID(player), button, time);
    }
    public static void AddButtonTimer(int playerID, InputButton button, float time)
    {
        playerButtonTimer[playerID, (int)button] = Time.time + time;
    }

    #endregion Gamepad

    #region Helpers

    public static bool GetAxisAndButton(Player player, InputAxis axis, InputAxisDir axisDir, InputButton button)
    {
        return GetAxisAndButton(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, button);
    }
    public static bool GetAxisAndButton(int playerID, InputAxis axis, InputAxisDir axisDir, InputButton button)
    {
        return GetButton(playerID, button) || GetAxisDir(playerID, axis, axisDir);
    }

    public static bool GetAxisAndButton(Player player, InputAxis axis, InputAxisDir axisDir, InputButton button, float delay)
    {
        return GetAxisAndButton(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, button, delay);
    }
    public static bool GetAxisAndButton(int playerID, InputAxis axis, InputAxisDir axisDir, InputButton button, float delay)
    {
        if ((GetButton(playerID, button) || GetAxisDir(playerID, axis, axisDir)) && CanPressAgainRaw(playerID, button, delay) && CanMoveAgainRaw(playerID, axis, axisDir, delay))
        {
            AddAxisTimer(playerID, axis, axisDir, delay);
            AddButtonTimer(playerID, button, delay);

            return true;
        }

        return false;
    }

    public static bool GetAxisAndButtonRaw(Player player, InputAxis axis, InputAxisDir axisDir, InputButton button, float delay)
    {
        return GetAxisAndButtonRaw(PlayerManager.Instance.GetPlayerID(player), axis, axisDir, button, delay);
    }
    public static bool GetAxisAndButtonRaw(int playerID, InputAxis axis, InputAxisDir axisDir, InputButton button, float delay)
    {
        return (GetButton(playerID, button) || GetAxisDir(playerID, axis, axisDir)) && CanPressAgainRaw(playerID, button, delay) && CanMoveAgainRaw(playerID, axis, axisDir, delay);
    }

    public static bool GetAxes(Player player, InputAxisDir axesDir)
    {
        return GetAxes(PlayerManager.Instance.GetPlayerID(player), axesDir);
    }
    public static bool GetAxes(int playerID, InputAxisDir axesDir)
    {
        return GetAxes(playerID, axesDir, axesDir);
    }

    public static bool GetAxes(Player player, InputAxisDir leftAxisDir, InputAxisDir rightAxisDir)
    {
        return GetAxes(PlayerManager.Instance.GetPlayerID(player), leftAxisDir, rightAxisDir);
    }
    public static bool GetAxes(int playerID, InputAxisDir leftAxisDir, InputAxisDir rightAxisDir)
    {
        return GetAxisDir(playerID, InputAxis.Left, leftAxisDir) && GetAxisDir(playerID, InputAxis.Right, rightAxisDir);
    }

    public static bool GetButtons(Player player, params InputButton[] buttons)
    {
        return GetButtons(PlayerManager.Instance.GetPlayerID(player), buttons);
    }
    public static bool GetButtons(int playerID, params InputButton[] buttons)
    {
        foreach (InputButton button in buttons)
        {
            if (!GetButton(playerID, button)) return false;
        }

        return true;
    }

    public static bool GetButtons(Player player, float delay, params InputButton[] buttons)
    {
        return GetButtons(PlayerManager.Instance.GetPlayerID(player), delay, buttons);
    }
    public static bool GetButtons(int playerID, float delay, params InputButton[] buttons)
    {
        foreach (InputButton button in buttons)
        {
            if (!GetButton(playerID, button)) return false;
            if (!CanPressAgainRaw(playerID, button, delay)) return false;
        }

        foreach (InputButton button in buttons)
        {
            AddButtonTimer(playerID, button, 0.0f);
        }

        return true;
    }

    public static bool GetButtonsRaw(Player player, float delay, params InputButton[] buttons)
    {
        return GetButtonsRaw(PlayerManager.Instance.GetPlayerID(player), delay, buttons);
    }
    public static bool GetButtonsRaw(int playerID, float delay, params InputButton[] buttons)
    {
        foreach (InputButton button in buttons)
        {
            if (!GetButton(playerID, button)) return false;
            if (!CanPressAgainRaw(playerID, button, delay)) return false;
        }

        return true;
    }

    #endregion Helpers
}
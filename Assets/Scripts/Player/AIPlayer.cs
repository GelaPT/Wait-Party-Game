using UnityEngine;

public class AIPlayer : Player
{
    public bool[] FakeInput { get; set; } = new bool[16];
    public Vector2[] FakeInputAxis { get; set; } = new Vector2[2];

    public AIPlayer() : base()
    {
        InputLateUpdate();
    }

    public void InputLateUpdate()
    {
        for (int i = 0; i < 16; i++)
        {
            FakeInput[i] = false;
        }

        for (int i = 0; i < 2; i++)
        {
            FakeInputAxis[i] = Vector2.zero;
        }
    }

    public void PressButton(InputButton button)
    {
        FakeInput[(int)button] = true;
    }

    public void MoveAxis(InputAxis axis, Vector2 fakeAxis)
    {
        FakeInputAxis[(int)axis] = fakeAxis;
    }

    public void MoveAxis(InputAxis axis, InputAxisDir fakeAxisDir)
    {
        FakeInputAxis[(int)axis] = fakeAxisDir switch
        {
            InputAxisDir.N => new Vector2(0, 1),
            InputAxisDir.NE => new Vector2(1, 1),
            InputAxisDir.E => new Vector2(1, 0),
            InputAxisDir.SE => new Vector2(1, -1),
            InputAxisDir.S => new Vector2(0, -1),
            InputAxisDir.SW => new Vector2(-1, -1),
            InputAxisDir.W => new Vector2(-1, 0),
            InputAxisDir.NW => new Vector2(-1, 1),
            InputAxisDir.None => new Vector2(0, 0),
            _ => new Vector2(0, 0)
        };
    }
}
using UnityEngine.InputSystem;

public class OpeningUIPanel : UIPanel
{
    protected override void Update()
    {
        base.Update();

        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.startButton.ReadValue() == 1)
            {
                UIManager.Instance.SwitchPanel(UIManager.Instance.panels[1]);
                PlayerManager.Instance.MakeHost(gamepad);
            }
        }
    }
}
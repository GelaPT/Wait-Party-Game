using UnityEngine.InputSystem;

public class OpeningUIPanel : UIPanel
{
    private void Update()
    {
        if (UIManager.Instance.CurrentPanel != this) return;

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
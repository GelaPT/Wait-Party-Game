using UnityEngine.InputSystem;

public class OpeningUIPanel : UIPanel
{
    private void Update()
    {
        UIManager uiManager = UIManager.Instance;
        if (uiManager.CurrentPanel != this) return;

        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.startButton.ReadValue() == 1)
            {
                uiManager.SwitchPanel(uiManager.panels[1]);
                PlayerManager.Instance.MakeHost(gamepad);
            }
        }
    }
}
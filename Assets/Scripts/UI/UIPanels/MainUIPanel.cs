using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUIPanel : UIPanel
{
    private void Update()
    {
        if (UIManager.Instance.CurrentPanel != this) return;

        EventSystem eventSystem = EventSystem.current;

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.S, InputButton.Down, 0.1f))
        {
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Button>().FindSelectableOnDown()?.gameObject);
            AudioManager.Instance.PlaySound("ui_select");
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.N, InputButton.Up, 0.1f))
        {
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Button>().FindSelectableOnUp()?.gameObject);
            AudioManager.Instance.PlaySound("ui_select");
        }

        if(InputManager.GetButton(0, InputButton.A, 0.3f))
        {
            eventSystem?.currentSelectedGameObject?.GetComponent<Button>().onClick.Invoke();
            AudioManager.Instance.PlaySound("ui_click");
        }
    }
}

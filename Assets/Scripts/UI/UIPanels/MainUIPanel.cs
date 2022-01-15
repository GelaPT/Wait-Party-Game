using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUIPanel : UIPanel
{
    private void Update()
    {
        if (UIManager.Instance.CurrentPanel != this) return;

        EventSystem eventSystem = EventSystem.current;

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.S, InputButton.Down))
        {
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Button>().FindSelectableOnDown().gameObject);
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.N, InputButton.Up))
        {
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Button>().FindSelectableOnUp().gameObject);
        }

        if(InputManager.GetButton(0, InputButton.A, 0.3f))
        {
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}

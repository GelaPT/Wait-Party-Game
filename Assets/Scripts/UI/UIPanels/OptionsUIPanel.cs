using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OptionsUIPanel : UIPanel
{
    public override void ClosePanel()
    {
        base.ClosePanel();

        // Save Options
    }

    public override void OpenPanel()
    {
        base.OpenPanel();

        // Load Options
    }

    private void Update()
    {
        if (UIManager.Instance.CurrentPanel != this) return;

        EventSystem eventSystem = EventSystem.current;

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.S, InputButton.Down, 0.1f))
        {
            Selectable selectable;
            if((selectable = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown()) != null)
            {
                eventSystem.SetSelectedGameObject(selectable.gameObject);
            }
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.N, InputButton.Up, 0.1f))
        {
            Selectable selectable;
            if((selectable = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp()) != null)
            {
                eventSystem.SetSelectedGameObject(selectable.gameObject);
            }
        }

        if (eventSystem.currentSelectedGameObject.TryGetComponent(out Slider slider))
        {
            if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.W, InputButton.Left, 0.1f))
            {
                slider.value -= 0.1f;
            }

            if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.E, InputButton.Right, 0.1f))
            {
                slider.value += 0.1f;
            }
        }

        if (eventSystem.currentSelectedGameObject.TryGetComponent(out Toggle toggle))
        {
            if (InputManager.GetButton(0, InputButton.A, 0.3f))
            {
                toggle.isOn = !toggle.isOn;
            }

            if(toggle.name != "Fullscreen")
            {
                if(InputManager.GetButton(0, InputButton.B, 0.1f)) { }
            }
        }

        if(eventSystem.currentSelectedGameObject.TryGetComponent(out TMP_Dropdown dropdown))
        {
            if (InputManager.GetButton(0, InputButton.A, 0.3f))
            {
                dropdown.Show();
            }
        }

        if (InputManager.GetButton(0, InputButton.B, 0.3f))
        {
            UIManager.Instance.SwitchToPreviousPanel();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class OptionsUIPanel : UIPanel
{
    public TMP_Dropdown resolutionDropdown;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;

    public override void ClosePanel()
    {
        base.ClosePanel();
        UIManager.Instance.PlayCameraTrigger("Main");
        SaveManager.SaveData(OptionsManager.Instance.gameOptions);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();

        GameOptions gameOptions = OptionsManager.Instance.gameOptions;

        for (int i = 0; i < resolutionDropdown.options.Count; i++)
        {
            if (OptionsManager.Instance.allowedResolutions[i]) continue;

            Debug.Log(i + ": " + resolutionDropdown.options[i].text);
            resolutionDropdown.options.RemoveAt(i);
        }

        resolutionDropdown.value = (int)gameOptions.resolution;

        musicVolumeSlider.value = gameOptions.musicVolume;
        sfxVolumeSlider.value = gameOptions.sfxVolume;
        fullscreenToggle.isOn = gameOptions.fullscreen;
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

    public void OnSFXSliderChanged(float value)
    {
        value = (float)Math.Round(value, 1);
        OptionsManager.Instance.ChangeSFXVolume(value);
    }

    public void OnMusicSliderChanged(float value)
    {
        value = (float)Math.Round(value, 1);
        OptionsManager.Instance.ChangeMusicVolume(value);
    }

    public void OnFullscreenChange(bool value)
    {
        OptionsManager.Instance.ChangeFullscreen(value);
    }

    public void OnDropdownChange(int value)
    {
        Debug.Log(value);
        OptionsManager.Instance.ChangeResolution((Resolutions)value);
    }
}

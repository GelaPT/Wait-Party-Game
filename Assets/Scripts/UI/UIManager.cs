using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Animator CameraAnimator;

    [HideInInspector] public List<UIPanel> panels = new();

    public UIPanel CurrentPanel { get; private set; }

    public UIPanel PreviousPanel { get; private set; }

    public UIPanel openingPanel;

    [Header("Events")]
    public UnityEvent onPanelChanged = new();

    protected override void Awake()
    {
        base.Awake();
        GetComponentsInChildren(true, panels);
        StartCoroutine("TryFindCamera");
    }

    IEnumerator TryFindCamera()
    {
        yield return new WaitUntil(() => LobbySceneManager.Instance != null);
        CameraAnimator = LobbySceneManager.Instance.cameraAnimator;
    }

    private void Start()
    {
        SwitchPanel(openingPanel);
    }

    public UIPanel GetPanel(string name)
    {
        foreach(UIPanel panel in panels)
        {
            if (panel.name == name) return panel;
        }

        return null;
    }

    public void SwitchPanel(string panelName)
    {
        UIPanel panel = GetPanel(panelName);

        if (panel == null)
        {
            Debug.LogError("Panel " + panelName + " not found!");
            return;
        }

        SwitchPanel(panel);
    }

    public void SwitchPanel(UIPanel uiPanel)
    {
        if (!uiPanel) return;
        if (uiPanel == CurrentPanel) return;

        if (CurrentPanel)
        {
            CurrentPanel.ClosePanel();
            PreviousPanel = CurrentPanel;
        }

        CurrentPanel = uiPanel;
        CurrentPanel.OpenPanel();

        if(onPanelChanged != null)
        {
            onPanelChanged.Invoke();
        }
    }

    public void SwitchToPreviousPanel()
    {
        if (!PreviousPanel) return;

        SwitchPanel(PreviousPanel);
    }

    public void PlayCameraTrigger(string trigger)
    {
        CameraAnimator.SetTrigger(trigger);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

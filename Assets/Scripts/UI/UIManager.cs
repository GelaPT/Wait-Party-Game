using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Camera UICamera;

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
    }

    private void Start()
    {
        SwitchPanel(openingPanel);
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

    public void LoadScene(string scene)
    {
        UICamera.gameObject.SetActive(false);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

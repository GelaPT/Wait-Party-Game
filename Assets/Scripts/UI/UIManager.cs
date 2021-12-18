using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    public UnityEvent onPanelChanged = new();

    private List<UIPanel> panels = new();

    public UIPanel openingPanel;

    private UIPanel currentPanel;
    public UIPanel CurrentPanel { get => currentPanel; }

    private UIPanel previousPanel;
    public UIPanel PreviousPanel { get => previousPanel; }

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
        if (uiPanel == currentPanel) return;

        if (currentPanel)
        {
            currentPanel.ClosePanel();
            previousPanel = currentPanel;
        }

        currentPanel = uiPanel;
        currentPanel.OpenPanel();

        if(onPanelChanged != null)
        {
            onPanelChanged.Invoke();
        }
    }

    public void SwitchToPreviousPanel()
    {
        if (!previousPanel) return;

        SwitchPanel(previousPanel);
    }
}

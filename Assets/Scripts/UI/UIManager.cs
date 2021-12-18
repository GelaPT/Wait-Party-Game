using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public UnityEvent onPanelChanged = new();

    private List<UIPanel> panels = new();

    private UIPanel currentPanel;
    public UIPanel CurrentPanel { get => currentPanel; }

    private UIPanel previousPanel;
    public UIPanel PreviousPanel { get => previousPanel; }

    void Start()
    {
        GetComponentsInChildren(true, panels);
    }

    public void SwitchPanel(UIPanel uiPanel)
    {
        if (!uiPanel) return;

        if (currentPanel)
        {
            currentPanel.ClosePanel();
            previousPanel = currentPanel;
        }

        currentPanel = uiPanel;
        currentPanel.gameObject.SetActive(true);
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

    public void LoadPanel(UIPanel panel)
    {
        StartCoroutine(WaitForPanelLoaded(panel));
    }

    private IEnumerator WaitForPanelLoaded(UIPanel panel)
    {
        yield return null;
    }
}

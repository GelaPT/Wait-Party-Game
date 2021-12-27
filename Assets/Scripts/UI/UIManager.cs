using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    private PlayerManager playerManager;

    [HideInInspector] public List<UIPanel> panels = new();

    private UIPanel currentPanel;
    public UIPanel CurrentPanel { get => currentPanel; }

    private UIPanel previousPanel;
    public UIPanel PreviousPanel { get => previousPanel; }

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
        playerManager = PlayerManager.Instance;
        SwitchPanel(openingPanel);
    }

    private void Update()
    {
        if (currentPanel != openingPanel) return;
        
        foreach(var gamepad in Gamepad.all)
        {
            if(gamepad.buttonSouth.ReadValue() == 1)
            {
                SwitchPanel(panels[1]);
            }
        }
            
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

    public void RunParsec()
    {
        ParsecManager.Instance.GetAccessCode();
    }
}

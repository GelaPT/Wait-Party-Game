using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    public Selectable startSelectable;

    private UnityEvent onPanelOpen = new();
    private UnityEvent onPanelClose = new();

    private Animator animator;
    private CanvasGroup canvasGroup;

    void Start()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (startSelectable) EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
    }

    public virtual void OpenPanel()
    {
        if (onPanelOpen != null) onPanelOpen.Invoke();

        HandleAnimator("Open");
    }

    public virtual void ClosePanel()
    {
        if (onPanelClose != null) onPanelClose.Invoke();

        HandleAnimator("Close");
    }

    private void HandleAnimator(string trigger)
    {
        animator?.SetTrigger(trigger);
    }
}

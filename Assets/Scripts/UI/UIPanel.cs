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

    public UnityEvent onPanelOpen = new();
    public UnityEvent onPanelClose = new();

    private Animator animator;
    private CanvasGroup canvasGroup;

    [HideInInspector] public List<Selectable> selectables = new();

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();

        GetComponentsInChildren(true, selectables);

        if (startSelectable) EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
    }

    public virtual void OpenPanel()
    {
        if (onPanelOpen != null) onPanelOpen.Invoke();

        HandleAnimator(true);
    }

    public virtual void ClosePanel()
    {
        if (onPanelClose != null) onPanelClose.Invoke();

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        HandleAnimator(false);
    }

    protected void HandleAnimator(bool flag)
    {
        animator?.SetBool("Opened", flag);
    }
}
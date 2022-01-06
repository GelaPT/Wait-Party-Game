using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    protected AudioManager audioManager;

    public Selectable startSelectable;

    public UnityEvent onPanelOpen = new();
    public UnityEvent onPanelFinishOpen = new();
    public UnityEvent onPanelClose = new();

    protected Animator animator;
    protected CanvasGroup canvasGroup;

    protected bool isOpened = false;

    [HideInInspector] public List<Selectable> selectables = new();

    protected virtual void Awake()
    {
        audioManager = AudioManager.Instance;
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();

        GetComponentsInChildren(true, selectables);

        if (startSelectable) EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
    }

    public virtual void OpenPanel()
    {
        onPanelOpen?.Invoke();

        isOpened = true;

        HandleAnimator(true);
    }

    public virtual void ClosePanel()
    {
        onPanelClose?.Invoke();

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        isOpened = false;

        HandleAnimator(false);
    }

    public virtual void FinishedOpening()
    {
        onPanelFinishOpen?.Invoke();
    }

    protected void HandleAnimator(bool flag)
    {
        animator?.SetBool("Opened", flag);
    }
}
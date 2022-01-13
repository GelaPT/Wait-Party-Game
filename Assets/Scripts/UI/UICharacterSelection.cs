using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class UICharacterSelection : MonoBehaviour {
    private static List<UICharacterSelection> instances = new();
    public Button[] buttons;

    public int UICharacterIndex;
    public GameObject firstSelected;

    public GameObject JoinPanel;
    public GameObject SelectionPanel;
    public GameObject ReadyPanel;

    public EventSystem eventSystem;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI playerText;

    private float lastLeftPressed;
    private float lastRightPressed;
    private float lastUpPressed;
    private float lastDownPressed;

    private void Start()
    {
        lastLeftPressed = Time.time;
        lastRightPressed = Time.time;
        lastUpPressed = Time.time;
        lastDownPressed = Time.time;
        playerText.SetText("P"+(UICharacterIndex + 1));

        instances.Add(this);
        buttons = GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        if (!SelectionPanel.activeSelf) return;

        if(!eventSystem.currentSelectedGameObject.GetComponent<Selectable>().interactable)
        {
            eventSystem.SetSelectedGameObject(firstSelected);
        }

        Player player = PlayerManager.Instance.Players[UICharacterIndex];

        Vector2 LeftStick = InputManager.GetAxis(player, "left");

        if ((LeftStick.x < -0.9f || InputManager.GetButton(player, InputButton.Left)) && (Time.time - lastLeftPressed) > 0.2f)
        {
            lastLeftPressed = Time.time;

            if (eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().interactable)
            {
                eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().gameObject);
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstSelected);
            }
        }

        if ((LeftStick.x > 0.9f || InputManager.GetButton(player, InputButton.Right)) && (Time.time - lastRightPressed) > 0.2f)
        {
            lastRightPressed = Time.time;
            if (eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().interactable)
            {
                eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject);
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstSelected);
            }
        }

        if ((LeftStick.y > 0.9f || InputManager.GetButton(player, InputButton.Up)) && (Time.time - lastUpPressed) > 0.2f)
        {
            lastUpPressed = Time.time;
            if (eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp().interactable)
            {
                eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp().gameObject);
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstSelected);
            }
        }

        if ((LeftStick.y < -0.9f || InputManager.GetButton(player, InputButton.Down)) && (Time.time - lastDownPressed) > 0.2f)
        {
            lastDownPressed = Time.time;
            if (eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown().interactable)
            {
                eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown().gameObject);
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstSelected);
            }
        }
    }

    public void RegisterPlayer()
    {
        JoinPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelected);
    }

    public void UnregisterPlayer()
    {
        JoinPanel.SetActive(true);
        SelectionPanel.SetActive(false);
        eventSystem.SetSelectedGameObject(firstSelected);
        eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        nameText.SetText("AI");
    }

    public void ReadyPlayer()
    {
        if(eventSystem.currentSelectedGameObject.name != "B1")
        {
            ToggleButtonInteractibility(false);
        }

        SelectionPanel.SetActive(false);
        ReadyPanel.SetActive(true);
    }

    public void UnreadyPlayer()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            if (eventSystem.currentSelectedGameObject.name != "B1")
            {
                ToggleButtonInteractibility(true);
            }
        }

        SelectionPanel.SetActive(true);
        ReadyPanel.SetActive(false);
    }

    public void ToggleButtonInteractibility(bool flag)
    {
        foreach (UICharacterSelection characterSelection in instances)
        {
            foreach (Button button in characterSelection.buttons)
            {
                if (button.name == eventSystem.currentSelectedGameObject.name)
                {
                    button.interactable = flag;
                }
            }
        }
    }

    public void ChangeCharacter(GameObject character)
    {
        LobbySceneManager.Instance.UICharacterParents[UICharacterIndex].ChangeModel(character);
        nameText.SetText(character.name[0..^4]);
    }
}
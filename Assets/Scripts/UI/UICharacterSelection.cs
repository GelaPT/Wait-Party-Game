using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class UICharacterSelection : MonoBehaviour {
    public int uiCharacterIndex;
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
        playerText.SetText("P"+(uiCharacterIndex+1));
    }

    private void Update()
    {
        if (!SelectionPanel.activeSelf) return;
        Player player = PlayerManager.Instance.Players[uiCharacterIndex];

        Vector2 LeftStick = InputManager.GetAxis(player, "left");

        if ((LeftStick.x < -0.9f || InputManager.GetButton(player, InputButton.Left)) && (Time.time - lastLeftPressed) > 0.2f)
        {
            lastLeftPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        if ((LeftStick.x > 0.9f || InputManager.GetButton(player, InputButton.Right)) && (Time.time - lastRightPressed) > 0.2f)
        {
            lastRightPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        if ((LeftStick.y > 0.9f || InputManager.GetButton(player, InputButton.Up)) && (Time.time - lastUpPressed) > 0.2f)
        {
            lastUpPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp().gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        if ((LeftStick.y < -0.9f || InputManager.GetButton(player, InputButton.Down)) && (Time.time - lastDownPressed) > 0.2f)
        {
            lastDownPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown().gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void RegisterPlayer()
    {
        JoinPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        nameText.SetText("Random");
        eventSystem.SetSelectedGameObject(firstSelected);
    }

    public void ReadyPlayer()
    {
        SelectionPanel.SetActive(false);
        ReadyPanel.SetActive(true);
    }

    public void UnreadyPlayer()
    {
        SelectionPanel.SetActive(true);
        ReadyPanel.SetActive(false);
    }

    public void ChangeCharacter(GameObject character)
    {
        LobbySceneManager.Instance.uiCharacters[uiCharacterIndex].ChangeModel(character);
        nameText.SetText(character.name[0..^4]);
    }
}
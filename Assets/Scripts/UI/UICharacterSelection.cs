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

    public EventSystem eventSystem;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI playerText;

    private float lastLeftPressed = 0.0f;
    private float lastRightPressed = 0.0f;

    private void Start()
    {
        lastLeftPressed = Time.time;
        lastRightPressed = Time.time;
        playerText.SetText("P"+(uiCharacterIndex+1));
    }

    private void Update()
    {
        if (!SelectionPanel.activeSelf) return;
        Player player = PlayerManager.Instance.Players[uiCharacterIndex];

        if ((InputManager.GetAxis(player, "left").x < -0.9f || InputManager.GetButton(player, InputButton.Left)) && (Time.time - lastLeftPressed) > 0.3f)
        {
            lastLeftPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().gameObject);
        }

        if ((InputManager.GetAxis(player, "left").x > 0.9f || InputManager.GetButton(player, InputButton.Right)) && (Time.time - lastRightPressed) > 0.3f)
        {
            lastRightPressed = Time.time;
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject);
        }

        if(InputManager.GetButton(player, InputButton.A))
        {
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

    public void ChangeCharacter(GameObject character)
    {
        LobbySceneManager.Instance.uiCharacters[uiCharacterIndex].ChangeModel(character);
        nameText.SetText(character.name.Substring(0, character.name.Length - 4));
    }
}
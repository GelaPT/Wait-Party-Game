using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UICharacterSelection : MonoBehaviour {
    private static List<UICharacterSelection> instances = new();
    public Button[] buttons;

    public int UICharacterIndex;
    public GameObject firstSelected;
    private GameObject lastSelected;

    public GameObject JoinPanel;
    public GameObject SelectionPanel;
    public GameObject ReadyPanel;

    public EventSystem eventSystem;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI playerText;

    private void Start()
    {
        playerText.SetText("P"+(UICharacterIndex + 1));

        instances.Add(this);
        buttons = SelectionPanel.GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        if (!SelectionPanel.activeSelf) return;

        Player player = PlayerManager.Instance.Players[UICharacterIndex];

        SelectCharacter(player);
    }

    private void SelectCharacter(Player player)
    {
        if ((InputManager.GetAxisDir(player, InputAxis.Left, InputAxisDir.E) || InputManager.GetButton(player, InputButton.Right)) && InputManager.CanMoveAgainRaw(player, InputAxis.Left, InputAxisDir.E, 0.1f) && InputManager.CanPressAgainRaw(player, InputButton.Right, 0.1f))
        {
            InputManager.AddAxisTimer(player, InputAxis.Left, InputAxisDir.E, 0.1f);
            InputManager.AddButtonTimer(player, InputButton.Right, 0.1f);

            Selectable selectable = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight();

            while (!selectable.GetComponent<Button>().interactable)
            {
                selectable = selectable.FindSelectableOnRight();
            }

            eventSystem.SetSelectedGameObject(selectable.gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        if ((InputManager.GetAxisDir(player, InputAxis.Left, InputAxisDir.W) || InputManager.GetButton(player, InputButton.Left)) && InputManager.CanMoveAgainRaw(player, InputAxis.Left, InputAxisDir.W, 0.1f) && InputManager.CanPressAgainRaw(player, InputButton.Left, 0.1f))
        {
            InputManager.AddAxisTimer(player, InputAxis.Left, InputAxisDir.W, 0.1f);
            InputManager.AddButtonTimer(player, InputButton.Left, 0.1f);

            Selectable selectable = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft();
            
            while(!selectable.GetComponent<Button>().interactable)
            {
                selectable = selectable.FindSelectableOnLeft();
            }

            eventSystem.SetSelectedGameObject(selectable.gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        // TODO: Add Up and Down
    }

    public void RegisterPlayer()
    {
        JoinPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelected);
        nameText.SetText("Random");
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
        lastSelected = eventSystem.currentSelectedGameObject;

        if (lastSelected.name != "B1")
        {
            SetButtonInteractibility(lastSelected.name, false);
        }

        SelectionPanel.SetActive(false);
        ReadyPanel.SetActive(true);
        LobbySceneManager.Instance.UICharacterParents[UICharacterIndex].PlayReadyAnimation();
    }

    public void UnreadyPlayer()
    {
        SelectionPanel.SetActive(true);
        ReadyPanel.SetActive(false);

        if(lastSelected != null)
        {
            if (lastSelected.name != "B1")
            {
                SetButtonInteractibility(lastSelected.name, true);
            }

            eventSystem.SetSelectedGameObject(firstSelected);
            eventSystem.SetSelectedGameObject(lastSelected);
        }
        LobbySceneManager.Instance.UICharacterParents[UICharacterIndex].StopReadyAnimation();
    }

    public void SetButtonInteractibility(string buttonName, bool interactable)
    {
        
        foreach (UICharacterSelection characterSelection in instances)
        {
            if(!interactable && characterSelection != this)
            {
                EventSystem characterSelectionEventSystem = characterSelection.eventSystem;
                GameObject currentSelected = characterSelectionEventSystem.currentSelectedGameObject;
                if(currentSelected != null)
                {
                    if (currentSelected.name == buttonName)
                    {
                        Selectable selectable = currentSelected.GetComponent<Selectable>().FindSelectableOnRight();

                        while (!selectable.GetComponent<Button>().interactable)
                        {
                            selectable = selectable.FindSelectableOnRight();
                        }

                        characterSelectionEventSystem.SetSelectedGameObject(selectable.gameObject);
                        characterSelectionEventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }

            foreach (Button button in characterSelection.buttons)
            {
                if (button.name == buttonName)
                {
                    button.interactable = interactable;
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